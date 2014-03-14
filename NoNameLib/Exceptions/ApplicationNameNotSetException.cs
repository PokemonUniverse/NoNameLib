namespace NoNameLib.Exceptions
{
    /// <summary>
    /// Exception class for when the application info has not been set
    /// </summary>
    public class ApplicationNameNotSetException : TechnicalException
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the Dionysos.Exceptions.ApplicationNameNotSetException class
        /// </summary>	
        public ApplicationNameNotSetException(string message)
            : base(message)
        {
        }

        #endregion
    }
}
