using System;
using NoNameLib.Extension;

namespace NoNameLib.Exceptions
{
    /// <summary>
    /// Exception class used for throwing technical exceptions
    /// </summary>
    public class TechnicalException : ApplicationException
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the Lynx_media.TechnicalException class
        /// </summary>
        /// <param name="message"></param>
        public TechnicalException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructs an instance of the Lynx_media.TechnicalException class
        /// </summary>
        /// <param name="message">Message format</param>
        /// <param name="args">Elements to format</param>
        public TechnicalException(string message, params object[] args)
            : base(message.FormatSafe(args))
        {
        }

        /// <summary>
        /// Constructs an instance of the Lynx_media.TechnicalException class
        /// </summary>
        /// <param name="innerException">Inner Exception</param>
        /// <param name="message">Message format</param>
        /// <param name="args">Elements to format</param>
        public TechnicalException(Exception innerException, string message, params object[] args)
            : base(message.FormatSafe(args), innerException)
        {
        }

        #endregion
    }
}
