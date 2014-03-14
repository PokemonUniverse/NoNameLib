using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameLib.Exceptions
{
    /// <summary>
    /// Exception class for when the application info has not been set
    /// </summary>
    public class ApplicationInfoNotSetException : TechnicalException
    {
        #region Constructors

        /// <summary>
        /// Constructs an instance of the Lynx_media.Exceptions.ApplicationInfoNotSetException class
        /// </summary>	
        public ApplicationInfoNotSetException(string message)
            : base(message)
        {
        }

        #endregion
    }
}
