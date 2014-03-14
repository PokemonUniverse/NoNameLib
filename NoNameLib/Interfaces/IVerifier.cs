using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameLib.Interfaces
{
    /// <summary>
    /// Interface which is used to implement verifiers.
    /// </summary>
    public interface IVerifier
    {
        #region Properties

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        string ErrorMessage
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the verifier.
        /// </summary>
        /// <returns><c>true</c> if verification was succesful; otherwise <c>false</c>.</returns>
        bool Verify();

        #endregion
    }
}
