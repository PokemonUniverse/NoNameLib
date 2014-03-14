using NoNameLib.Exceptions;
using NoNameLib.Interfaces;

namespace NoNameLib
{
    /// <summary>
    /// Class which contains information about a webapplication
    /// </summary>
    public class ApplicationInfo : IApplicationInfo
    {
        #region Fields

        private string applicationName = string.Empty;
        private string applicationVersion = string.Empty;
        private string basePath = string.Empty;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the application
        /// </summary>
        public string ApplicationName
        {
            get
            {
                if (Instance.Empty(this.applicationName))
                {
                    throw new ApplicationNameNotSetException("Application name not set in application start! Check the initialization of the Global.ApplicationInfo.");
                }
                return this.applicationName;
            }
            set
            {
                this.applicationName = value;
            }
        }

        /// <summary>
        /// Gets or sets the version of the application
        /// </summary>
        public string ApplicationVersion
        {
            get
            {
                return this.applicationVersion;
            }
            set
            {
                this.applicationVersion = value;
            }
        }

        /// <summary>
        /// Gets or sets the base path for this application
        /// </summary>
        public string BasePath
        {
            get
            {
                return this.basePath;
            }
            set
            {
                this.basePath = value;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a generic string instance containing information about a application
        /// </summary>
        /// <returns>A string instance containg application information</returns>
        public new string ToString()
        {
            return string.Empty;
        }

        /// <summary>
        /// Converts the content of this ApplicationInfo instance to XML
        /// </summary>
        public void ToXML()
        {
        }

        #endregion
    }
}
