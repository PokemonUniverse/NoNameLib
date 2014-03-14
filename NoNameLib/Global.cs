using NoNameLib.Configuration;
using NoNameLib.Exceptions;
using NoNameLib.Interfaces;
using NoNameLib.Logging;

namespace NoNameLib
{
    public class Global
    {
        private IApplicationInfo applicationInfo;
        private ConfigurationInfoCollection configurationInfo = new ConfigurationInfoCollection();
        private IConfigurationProvider configurationProvider;
        private ILoggingProvider loggingProvider;

        #region Constructor / Singleton

        private static readonly Global instance = new Global();

        private Global()
        {
        }

        #endregion

        /// <summary>
        /// Gets or sets the Dionysos.Interfaces.IApplicationInfo instance which contains information about the application
        /// </summary>
        public static IApplicationInfo ApplicationInfo
        {
            get
            {
                if (Instance.Empty(instance.applicationInfo))
                {
                    throw new ApplicationInfoNotSetException("Global.ApplicationInfo is not set!");
                }
                return instance.applicationInfo;
            }
            set { instance.applicationInfo = value; }
        }

        /// <summary>
        /// Gets or sets the Dionysos.Configuration.ConfigurationInfoCollection instance
        /// </summary>
        public static ConfigurationInfoCollection ConfigurationInfo
        {
            get { return instance.configurationInfo; }
            set { instance.configurationInfo = value; }
        }

        /// <summary>
        /// Gets or sets the IConfigurationProvider instance
        /// </summary>
        public static IConfigurationProvider ConfigurationProvider
        {
            get
            {
                if (Instance.Empty(instance.configurationProvider))
                {
                    throw new ConfigurationProviderNotSetException("Global.ConfigurationProvider is not set!");
                }
                
                return instance.configurationProvider;
            }
            set { instance.configurationProvider = value; }
        }

        /// <summary>
        /// Gets or sets the ILoggingProvider instance
        /// </summary>
        public static ILoggingProvider LoggingProvider
        {
            get
            {
                if (Instance.Empty(instance.loggingProvider))
                {
                    throw new LoggingProviderNotSetException("Global.LoggingProvider is not set!");
                }

                return instance.loggingProvider;
            }
            set { instance.loggingProvider = value; }
        }
    }
}
