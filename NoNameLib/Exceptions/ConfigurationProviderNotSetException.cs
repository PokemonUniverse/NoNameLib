namespace NoNameLib.Exceptions
{
    /// <summary>
    /// Exception class for when the configuration provider has not been set
    /// </summary>
    public class ConfigurationProviderNotSetException : TechnicalException
    {
        #region Fields

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the Lynx_media.Exceptions.ApplicationInfoNotSetException class
        /// </summary>	
        public ConfigurationProviderNotSetException(string message)
            : base(message)
        {
        }

        #endregion
    }
}
