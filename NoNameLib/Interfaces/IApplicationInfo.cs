using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameLib.Interfaces
{
    /// <summary>
    /// Interface for application information objects
    /// </summary>
    public interface IApplicationInfo
    {
        #region Methods

        /// <summary>
        /// Returns a generic string instance containing information about an application
        /// </summary>
        /// <returns>A string containg application information</returns>
        string ToString();

        /// <summary>
        /// Converts the information about an application to XML
        /// </summary>
        void ToXML();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of the application
        /// </summary>
        string ApplicationName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the application version.
        /// </summary>
        /// <value>
        /// The application version.
        /// </value>
        string ApplicationVersion
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the base path for this application
        /// </summary>
        string BasePath
        {
            get;
            set;
        }

        #endregion
    }
}
