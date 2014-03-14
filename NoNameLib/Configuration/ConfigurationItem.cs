using System;

namespace NoNameLib.Configuration
{
    /// <summary>
    /// Configuration class which represents an application settings item
    /// </summary>
    public class ConfigurationItem
    {
        #region Fields

        private readonly string section = String.Empty;
        private readonly string name = String.Empty;
        private readonly string friendlyName = String.Empty;
        private readonly object defaultValue;
        private readonly Type type;
        private readonly bool applicationSpecific;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the ConfigurationItem class.
        /// </summary>
        /// <param name="section">The section to which this configuration item belongs to.</param>
        /// <param name="name">The name of the configuration item.</param>
        /// <param name="friendlyName">The friendly name of the configuration item.</param>
        /// <param name="defaultValue">The default value of the configuration item.</param>
        /// <param name="type">The value type of the configuration item.</param>
        public ConfigurationItem(string section, string name, string friendlyName, object defaultValue, Type type)
        {
            this.section = section;
            this.name = name;
            this.friendlyName = friendlyName;
            this.defaultValue = defaultValue;
            this.type = type;
        }

        /// <summary>
        /// Initializes a new instance of the ConfigurationItem class.
        /// </summary>
        /// <param name="section">The section to which this configuration item belongs to.</param>
        /// <param name="name">The name of the configuration item.</param>
        /// <param name="friendlyName">The friendly name of the configuration item.</param>
        /// <param name="defaultValue">The default value of the configuration item.</param>
        /// <param name="type">The value type of the configuration item.</param>
        /// <param name="applicationSpecific">Indicates whether this configuration item is application specific.</param>
        public ConfigurationItem(string section, string name, string friendlyName, object defaultValue, Type type, bool applicationSpecific)
            : this(section, name, friendlyName, defaultValue, type)
        {
            this.applicationSpecific = applicationSpecific;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the section.
        /// </summary>
        public string Section
        {
            get
            {
                return this.section;
            }
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get
            {
                return this.name;
            }
        }

        /// <summary>
        /// Gets the friendly name.
        /// </summary>
        public string FriendlyName
        {
            get
            {
                return this.friendlyName;
            }
        }

        /// <summary>
        /// Gets the default value.
        /// </summary>
        public object DefaultValue
        {
            get
            {
                return this.defaultValue;
            }
        }

        /// <summary>
        /// Gets the value type.
        /// </summary>
        public Type Type
        {
            get
            {
                return this.type;
            }
        }

        /// <summary>
        /// Indicates whether the configuration item is application specific.
        /// </summary>
        public bool ApplicationSpecific
        {
            get
            {
                return this.applicationSpecific;
            }
        }

        #endregion
    }
}
