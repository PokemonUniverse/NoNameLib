using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NoNameLib.Exceptions;
using NoNameLib.Extension;
using NoNameLib.Interfaces;

namespace NoNameLib.Configuration
{
    /// <summary>
    /// Configuration provider class which is used for reading/writing configuration values from xml files
    /// </summary>
    public class XmlConfigurationProvider : IConfigurationProvider
    {
        #region Fields

        /// <summary>
        /// The default config file.
        /// </summary>
        private const string DEFAULT_CONFIG_FILE = "AppSettings.xml";

        /// <summary>
        /// The manual config file path.
        /// </summary>
        private string manualConfigFilePath = string.Empty;

        /// <summary>
        /// The locker object.
        /// </summary>
        private readonly object locker = new object();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the path to the configuration file override
        /// </summary>
        public string ManualConfigFilePath
        {
            get
            {
                return this.manualConfigFilePath;
            }
            set
            {
                this.manualConfigFilePath = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Get XML Document
        /// </summary>
        /// <returns></returns>
        private XmlDocument GetXmlDocument()
        {
            return this.GetXmlDocument(this.GetXmlDocumentFullPath());
        }

        /// <summary>
        /// Get XML Document
        /// </summary>
        /// <returns></returns>
        private string GetXmlDocumentFullPath()
        {
            if (this.manualConfigFilePath.Length > 0)
                return this.manualConfigFilePath;
            
            return Path.Combine(Global.ApplicationInfo.BasePath, DEFAULT_CONFIG_FILE);
        }

        /// <summary>
        /// Loads the XML.
        /// </summary>
        /// <param name="configFile">The config file.</param>
        /// <returns></returns>
        protected virtual string LoadXml(string configFile)
        {
            if (File.Exists(configFile))
                return File.ReadAllText(configFile);
            
            return string.Empty;
        }

        /// <summary>
        /// Saves the XML.
        /// </summary>
        /// <param name="configFile">The config file.</param>
        /// <param name="doc">The doc.</param>
        private void SaveXml(string configFile, XmlDocument doc)
        {
            string xml;
            using (var stringWriter = new StringWriter())
            {
                var settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = "  ";
                settings.Encoding = Encoding.UTF8;
                using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
                {
                    doc.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    xml = stringWriter.GetStringBuilder().ToString();
                }
            }
            this.SaveXml(configFile, xml);
        }

        /// <summary>
        /// Saves the XML.
        /// </summary>
        /// <param name="configFile">The config file.</param>
        /// <param name="xml">The XML.</param>
        protected virtual void SaveXml(string configFile, string xml)
        {
            File.WriteAllText(configFile, xml);
        }

        /// <summary>
        /// Saves the values of the application settings instance to disk using the specified filename
        /// </summary>
        /// <param name="configFile">The path to write the configuration file to</param>
        /// <returns>True if saving was succesful, False if not</returns>
        private XmlDocument GetXmlDocument(string configFile)
        {
            // Create and initialize a XmlDocument instance
            lock (locker)
            {
                var doc = new XmlDocument();
                string xml = this.LoadXml(configFile);
                if (xml.IsNullOrWhiteSpace())
                {
                    // Create the top node and append it to the XmlDocument instance
                    XmlNode appSettingsNode = doc.CreateNode(XmlNodeType.Element, "configuration", string.Empty);
                    doc.AppendChild(appSettingsNode);

                    // Walk through the application settings sections
                    foreach (ConfigurationItemCollection t in Global.ConfigurationInfo)
                    {
                        var configurationItems = t.OrderBy(ci => ci.Name).ThenBy(ci => ci.Section).ToList();

                        string currentSectionName = string.Empty;
                        if (configurationItems.Any())
                        {
                            XmlNode sectionNode = null;

                            for (int j = 0; j < configurationItems.Count(); j++)
                            {
                                var configurationItem = configurationItems[j];

                                if (configurationItems[j].Section != currentSectionName)
                                {
                                    sectionNode = doc.CreateNode(XmlNodeType.Element, "section", string.Empty);
                                    XmlAttribute sectionName = doc.CreateAttribute("name");
                                    sectionName.Value = configurationItems[j].Section;
                                    if (sectionNode.Attributes != null) sectionNode.Attributes.Append(sectionName);
                                    appSettingsNode.AppendChild(sectionNode);
                                    currentSectionName = configurationItems[j].Section;
                                }

                                XmlNode itemNode = doc.CreateNode(XmlNodeType.Element, "item", string.Empty);
                                XmlAttribute itemName = doc.CreateAttribute("name");
                                itemName.InnerText = configurationItem.Name;
                                if (itemNode.Attributes != null)
                                {
                                    itemNode.Attributes.Append(itemName);
                                    if (configurationItem.ApplicationSpecific)
                                    {
                                        XmlAttribute itemApplicationSpecific = doc.CreateAttribute("applicationName");
                                        itemApplicationSpecific.InnerText = Global.ApplicationInfo.ApplicationName;
                                        itemNode.Attributes.Append(itemApplicationSpecific);
                                    }
                                }

                                XmlNode itemFriendlyName = doc.CreateNode(XmlNodeType.Element, "friendlyname", string.Empty);
                                itemFriendlyName.InnerText = configurationItem.FriendlyName;
                                itemNode.AppendChild(itemFriendlyName);

                                XmlNode type = doc.CreateNode(XmlNodeType.Element, "type", string.Empty);
                                type.InnerText = configurationItem.Type.ToString();
                                itemNode.AppendChild(type);

                                XmlNode value = doc.CreateNode(XmlNodeType.Element, "value", string.Empty);
                                value.InnerText = configurationItem.DefaultValue.ToString();
                                itemNode.AppendChild(value);

                                if (sectionNode != null) sectionNode.AppendChild(itemNode);
                            }
                        }
                    }

                    // Save the settings file to disk
                    this.SaveXml(configFile, doc);
                }
                else
                {
                    // Debug.WriteLine("Load XML");                    
                    doc.LoadXml(this.LoadXml(configFile));
                }

                return doc;
            }
        }

        /// <summary>
        /// Get's if the setting is defined for the current running application.
        /// This might not be the case when a certain set of ConfigInfo is not initialized in the application start.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>
        /// bool
        /// </returns>
        public bool IsDefined(string name)
        {
            ConfigurationItem configurationItem = this.GetConfigurationItem(name);

            // Check whether the configuration item information has been supplied
            return (configurationItem != null);
        }

        /// <summary>
        /// Gets configuration information for the specified configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item, based on the configuration constants</param>
        /// <returns>A Dionysos.Configuration.ConfigurationItem instance containing information about the configuration item</returns>
        private ConfigurationItem GetConfigurationItem(string name)
        {
            ConfigurationItem configurationItem = null;

            foreach (ConfigurationItemCollection configurationItems in Global.ConfigurationInfo)
            {
                foreach (ConfigurationItem temp in configurationItems.Where(temp => temp.Name.ToUpper() == name.ToUpper()))
                {
                    configurationItem = temp;
                    break;
                }

                if (!Instance.Empty(configurationItem))
                {
                    break;
                }
            }

            return configurationItem;
        }

        private XmlNode RetrieveXmlItemNode(string name, bool? applicationSpecific, XmlDocument doc)
        {
            XmlNode item;

            if (doc == null)
            {
                doc = this.GetXmlDocument();
            }
            var configurationItem = this.GetConfigurationItem(name);
            if (configurationItem == null)
            {
                throw new EmptyException(string.Format("No configuration item found for configuration constant '{0}'. Check the corresponding configuration info class.", name));
            }
            
            // Retrieven nodes
            string xmlSearchExpression;
            if (applicationSpecific ?? configurationItem.ApplicationSpecific)
                xmlSearchExpression = string.Format("//section[@name='{0}']/item[@name='{1}' and @applicationName='{2}']", configurationItem.Section, configurationItem.Name, Global.ApplicationInfo.ApplicationName);
            else
                xmlSearchExpression = string.Format("//section[@name='{0}']/item[@name='{1}' and not(@applicationName)]", configurationItem.Section, configurationItem.Name);

            XmlNodeList configItems = doc.SelectNodes(xmlSearchExpression);

            if (configItems == null || configItems.Count == 0)
            {
                item = null;
            }
            else if (configItems.Count > 1)
            {
                throw new TechnicalException(string.Format("Er is meer dan 1 ConfigurationItem gevonden in de xml configuratie file voor {0}.{1}", configurationItem.Section, configurationItem.Name));
            }
            else
            {
                item = configItems[0];
                if (item["value"] == null)
                {
                    throw new EmptyException(string.Format("XmlNode '{0}.{1}' does not have a field 'value'.", configurationItem.Section, configurationItem.Name));
                }
                if (item["friendlyname"] == null)
                {
                    throw new EmptyException(string.Format("XmlNode '{0}.{1}' does not have a field 'friendlyname'.", configurationItem.Section, configurationItem.Name));
                }
                if (item["type"] == null)
                {
                    throw new EmptyException(string.Format("XmlNode '{0}.{1}' does not have a field 'type'.", configurationItem.Section, configurationItem.Name));
                }
            }

            return item;
        }

        /// <summary>
        /// Gets an entity containing the configuration for the specified configuration section
        /// </summary>
        /// <param name="name">The name of the configuration value</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration item is application specific</param>
        /// <returns>An IEntity instance containing the configuration if the configuration value exists, null if not</returns>
        private string RetrieveXmlItemValue(string name, bool? applicationSpecific)
        {
            string toReturn = string.Empty;

            // Get the configruation item from the ConfigurationItemInfo collection
            // this contains the (meta)data (NOT the value) of the retrieved config item
            ConfigurationItem configurationItem = this.GetConfigurationItem(name);

            // Check whether the configuration item information has been supplied
            if (Instance.Empty(configurationItem))
            {
                throw new EmptyException(string.Format("No configuration item found for configuration constant '{0}'. Check the initialization of the configuration info in the Global Application class.", name));
            }
            if (Instance.Empty(configurationItem.Name))
            {
                throw new EmptyException(string.Format("Configuration item '{0}' does not have a name.", name));
            }
            if (Instance.Empty(configurationItem.Section))
            {
                throw new EmptyException(string.Format("Configuration item '{0}' does not have a section.", name));
            }
            
            XmlNode item = this.RetrieveXmlItemNode(name, applicationSpecific, null);
            if (item != null)
            {
                var xmlElement = item["value"];
                if (xmlElement != null) 
                    toReturn = xmlElement.InnerText;
            }

            return toReturn;
        }

        #endregion

        #region IConfigurationProvider Members

        /// <summary>
        /// Gets the current value of the specified configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.String instance containing the current value</returns>
        private string GetValue(string name)
        {
            return GetValue(name, (bool?)false);
        }

        /// <summary>
        /// Gets the current value of the specified configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.String instance containing the current value</returns>
        private string GetValue(string name, bool applicationSpecific)
        {
            return this.GetValue(name, (bool?)applicationSpecific);
        }

        /// <summary>
        /// Gets the current value of the specified configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.String instance containing the current value</returns>
        private string GetValue(string name, bool? applicationSpecific)
        {
            lock (locker)
            {
                string value = this.RetrieveXmlItemValue(name, applicationSpecific);
                if (Instance.Empty(value))
                {
                    try
                    {
                        value = this.GetConfigurationItem(name).DefaultValue.ToString();
                        this.SetValue(name, value, applicationSpecific);		
                    }
                    catch (Exception ex)
                    {
                        throw new TechnicalException("No default value could be found OR set for setting: " + name + "\n\r Error: " + ex.Message);
                    }
                }

                return value;
            }
        }

        /// <summary>
        /// Gets the current value of the specified application settings item
        /// </summary>
        /// <param name="item">The name of the application settings item</param>
        /// <returns>A System.Object instance containing the current value</returns>
        private string GetValueRefreshed(string item)
        {
            return this.GetValue(item);
        }

        /// <summary>
        /// Gets the current value of the specified application settings item
        /// </summary>
        /// <param name="item">The name of the application settings item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>A System.Object instance containing the current value</returns>
        private string GetValueRefreshed(string item, bool applicationSpecific)
        {
            return this.GetValue(item, applicationSpecific);
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Boolean instance containing the current value</returns>
        public bool GetBool(string name)
        {
            return Boolean.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Boolean instance containing the current value</returns>
        public bool GetBool(string name, bool applicationSpecific)
        {
            return Boolean.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Boolean instance containing the current value</returns>
        public bool GetBoolRefreshed(string name)
        {
            return Boolean.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Boolean instance containing the current value</returns>
        public bool GetBoolRefreshed(string name, bool applicationSpecific)
        {
            return Boolean.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Byte instance containing the current value</returns>
        public byte GetByte(string name)
        {
            return Byte.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Byte instance containing the current value</returns>
        public byte GetByte(string name, bool applicationSpecific)
        {
            return Byte.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Byte instance containing the current value</returns>
        public byte GetByteRefreshed(string name)
        {
            return Byte.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Byte instance containing the current value</returns>
        public byte GetByteRefreshed(string name, bool applicationSpecific)
        {
            return Byte.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Char instance containing the current value</returns>
        public char GetChar(string name)
        {
            return Char.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Char instance containing the current value</returns>
        public char GetChar(string name, bool applicationSpecific)
        {
            return Char.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Char instance containing the current value</returns>
        public char GetCharRefreshed(string name)
        {
            return Char.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Char instance containing the current value</returns>
        public char GetCharRefreshed(string name, bool applicationSpecific)
        {
            return Char.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.DateTime instance containing the current value</returns>
        public DateTime GetDateTime(string name)
        {
            return DateTime.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.DateTime instance containing the current value</returns>
        public DateTime GetDateTime(string name, bool applicationSpecific)
        {
            return DateTime.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.DateTime instance containing the current value</returns>
        public DateTime GetDateTimeRefreshed(string name)
        {
            return DateTime.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.DateTime instance containing the current value</returns>
        public DateTime GetDateTimeRefreshed(string name, bool applicationSpecific)
        {
            return DateTime.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Decimal instance containing the current value</returns>
        public decimal GetDecimal(string name)
        {
            return Decimal.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Decimal instance containing the current value</returns>
        public decimal GetDecimal(string name, bool applicationSpecific)
        {
            return Decimal.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Decimal instance containing the current value</returns>
        public decimal GetDecimalRefreshed(string name)
        {
            return Decimal.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Decimal instance containing the current value</returns>
        public decimal GetDecimalRefreshed(string name, bool applicationSpecific)
        {
            return Decimal.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Double instance containing the current value</returns>
        public double GetDouble(string name)
        {
            return Double.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Double instance containing the current value</returns>
        public double GetDouble(string name, bool applicationSpecific)
        {
            return Double.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Double instance containing the current value</returns>
        public double GetDoubleRefreshed(string name)
        {
            return Double.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Double instance containing the current value</returns>
        public double GetDoubleRefreshed(string name, bool applicationSpecific)
        {
            return Double.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Single instance containing the current value</returns>
        public float GetFloat(string name)
        {
            return Single.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Single instance containing the current value</returns>
        public float GetFloat(string name, bool applicationSpecific)
        {
            return Single.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Single instance containing the current value</returns>
        public float GetFloatRefreshed(string name)
        {
            return Single.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Single instance containing the current value</returns>
        public float GetFloatRefreshed(string name, bool applicationSpecific)
        {
            return Single.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Int32 instance containing the current value</returns>
        public int GetInt(string name)
        {
            return Int32.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Int32 instance containing the current value</returns>
        public int GetInt(string name, bool applicationSpecific)
        {
            return Int32.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Int32 instance containing the current value</returns>
        public int GetIntRefreshed(string name)
        {
            return Int32.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Int32 instance containing the current value</returns>
        public int GetIntRefreshed(string name, bool applicationSpecific)
        {
            return Int32.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Int64 instance containing the current value</returns>
        public long GetLong(string name)
        {
            return Int64.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Int64 instance containing the current value</returns>
        public long GetLong(string name, bool applicationSpecific)
        {
            return Int64.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Int64 instance containing the current value</returns>
        public long GetLongRefreshed(string name)
        {
            return Int64.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Int64 instance containing the current value</returns>
        public long GetLongRefreshed(string name, bool applicationSpecific)
        {
            return Int64.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Int16 instance containing the current value</returns>
        public short GetShort(string name)
        {
            return Int16.Parse(this.GetValue(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Int16 instance containing the current value</returns>
        public short GetShort(string name, bool applicationSpecific)
        {
            return Int16.Parse(this.GetValue(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.Int16 instance containing the current value</returns>
        public short GetShortRefreshed(string name)
        {
            return Int16.Parse(this.GetValueRefreshed(name));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Int16 instance containing the current value</returns>
        public short GetShortRefreshed(string name, bool applicationSpecific)
        {
            return Int16.Parse(this.GetValueRefreshed(name, applicationSpecific));
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.String instance containing the current value</returns>
        public string GetString(string name)
        {
            return this.GetValue(name);
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.String instance containing the current value</returns>
        public string GetString(string name, bool applicationSpecific)
        {
            return this.GetValue(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.String instance containing the current value</returns>
        public string GetStringRefreshed(string name)
        {
            return this.GetValueRefreshed(name);
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.String instance containing the current value</returns>
        public string GetStringRefreshed(string name, bool applicationSpecific)
        {
            return this.GetValueRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets a password
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public string GetPassword(string name)
        {
            return GetPassword(name, false);
        }

        /// <summary>
        /// Gets a password
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public string GetPassword(string name, bool applicationSpecific)
        {
            string value = this.GetValue(name, applicationSpecific);
            if (!value.IsNullOrWhiteSpace())
                value = Security.Cryptography.Cryptographer.DecryptStringUsingRijndael(value);

            return value;
        }

        /// <summary>
        /// Sets the value of the specified configuration item 
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="value">The value to set to the configuration item</param>
        public void SetValue(string name, object value)
        {
            this.SetValue(name, value, null);
        }

        /// <summary>
        /// Sets the value of the specified configuration item 
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <param name="value">The value to set to the configuration item</param>
        public void SetValue(string name, object value, bool applicationSpecific)
        {
            this.SetValue(name, value, (bool?)applicationSpecific);
        }

        /// <summary>
        /// Sets a password value
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        public void SetPassword(string name, string value)
        {
            SetPassword(name, value, false);
        }

        /// <summary>
        /// Sets a password value
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific. If null, uses the default value for the configuration item.</param>
        public void SetPassword(string name, string value, bool applicationSpecific)
        {
            if (!value.IsNullOrWhiteSpace())
                value = Security.Cryptography.Cryptographer.EncryptStringUsingRijndael(value);
            else
                value = string.Empty;

            this.SetValue(name, value);
        }

        /// <summary>
        /// Sets the value of the specified configuration item 
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <param name="value">The value to set to the configuration item</param>
        private void SetValue(string name, object value, bool? applicationSpecific)
        {
            lock (locker)
            {
                // First try to get the XML node at once            
                XmlDocument doc;
                doc = this.GetXmlDocument();
                XmlNode itemNode = this.RetrieveXmlItemNode(name, applicationSpecific, doc);

                if (itemNode != null)
                {
                    // YES, THE BEST CASE: JUST UPDATE THE VALUE
                    var xmlElement = itemNode["value"];
                    if (xmlElement != null) 
                        xmlElement.InnerText = value.ToString();
                }
                else
                {
                    // NO, A LOT TO DO AND CHECK
                    // Try to find the section first
                    ConfigurationItem configurationItem = this.GetConfigurationItem(name);
                    XmlNode sectionNode;

                    // Retrieven node
                    string xmlSearchExpression = string.Format("//section[@name='{0}']", configurationItem.Section);
                    XmlNodeList configSections = doc.SelectNodes(xmlSearchExpression);

                    // Check if we found teh section
                    if (configSections == null || configSections.Count == 0)
                    {
                        // Not found
                        sectionNode = null;
                    }
                    else if (configSections.Count > 1)
                    {
                        // Multiple found
                        throw new TechnicalException(string.Format("Er is meer dan 1 Section gevonden in de xml configuratie file voor {0}", configurationItem.Section));
                    }
                    else
                    {
                        // 1 found, succes!
                        sectionNode = configSections[0];
                    }

                    // Check if we found the section
                    if (sectionNode == null)
                    {
                        // Section not yet available, so add.
                        // First retrieve configuarion node
                        xmlSearchExpression = "/configuration";
                        XmlNodeList rootSections = doc.SelectNodes(xmlSearchExpression);
                        if (rootSections == null || rootSections.Count != 1)
                        {
                            throw new TechnicalException("Er zijn 0 of meerdere <configuration> elementen gevonden in appSettings.xml");
                        }

                        // We throw error if rootSection.Count != 1 so can savely use it here
                        XmlNode appSettingsNode = rootSections[0];

                        // Create and add section node
                        sectionNode = doc.CreateNode(XmlNodeType.Element, "section", string.Empty);
                        XmlAttribute sectionName = doc.CreateAttribute("name");
                        sectionName.Value = configurationItem.Section;
                        if (sectionNode.Attributes != null) sectionNode.Attributes.Append(sectionName);
                        appSettingsNode.AppendChild(sectionNode);
                    }

                    // We now have the section, let's add the item                
                    itemNode = doc.CreateNode(XmlNodeType.Element, "item", string.Empty);
                    XmlAttribute itemName = doc.CreateAttribute("name");
                    itemName.InnerText = configurationItem.Name;
                    if (itemNode.Attributes != null)
                    {
                        itemNode.Attributes.Append(itemName);
                        if (applicationSpecific ?? configurationItem.ApplicationSpecific)
                        {
                            XmlAttribute itemApplicationSpecific = doc.CreateAttribute("applicationName");
                            itemApplicationSpecific.InnerText = Global.ApplicationInfo.ApplicationName;
                            itemNode.Attributes.Append(itemApplicationSpecific);
                        }
                    }

                    XmlNode itemFriendlyName = doc.CreateNode(XmlNodeType.Element, "friendlyname", string.Empty);
                    itemFriendlyName.InnerText = configurationItem.FriendlyName;
                    itemNode.AppendChild(itemFriendlyName);

                    XmlNode type = doc.CreateNode(XmlNodeType.Element, "type", string.Empty);
                    type.InnerText = configurationItem.Type.ToString();
                    itemNode.AppendChild(type);

                    XmlNode valueElement = doc.CreateNode(XmlNodeType.Element, "value", string.Empty);
                    valueElement.InnerText = value.ToString();
                    itemNode.AppendChild(valueElement);

                    sectionNode.AppendChild(itemNode);
                }

                this.SaveXml(this.GetXmlDocumentFullPath(), doc);
            }
        }

        #endregion
    }
}
