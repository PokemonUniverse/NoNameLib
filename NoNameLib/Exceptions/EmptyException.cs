namespace NoNameLib.Exceptions
{
    /// <summary>
    /// Exception class for empty instances
    /// </summary>
    public class EmptyException : TechnicalException
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the Lynx_media.Exceptions.EmptyException class
        /// </summary>	
        public EmptyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructs an instance of the Lynx_media.EmptyException class
        /// </summary>
        /// <param name="message">Message format</param>
        /// <param name="args">Elements to format</param>
        public EmptyException(string message, params object[] args)
            : base(string.Format(message, args))
        {
        }

        #endregion
    }
}
