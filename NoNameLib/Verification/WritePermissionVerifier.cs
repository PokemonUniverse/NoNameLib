using System;
using System.IO;
using System.Security;
using System.Security.Permissions;
using NoNameLib.Interfaces;

namespace NoNameLib.Verification
{
    /// <summary>
    /// Generic Verifier to Check Write Permissions
    /// </summary>
    public class WritePermissionVerifier : IVerifier
    {
        #region Fields

        private string errorMessage = string.Empty;
        private readonly string fullPath = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the ExtraVestiging.Logic.WritePermissionEVAdminVerifier class
        /// </summary>
        public WritePermissionVerifier(string fullPath)
        {
            this.fullPath = fullPath;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Executes the verifier and checks whether there are permissions to write files to the archive directories
        /// </summary>
        /// <returns>True if verification was succesful, False if not</returns>
        public bool Verify()
        {
            this.ValidatePath(this.fullPath);
            return this.errorMessage.Length == 0;
        }

        private void ValidatePath(string path)
        {
            try
            {
                // Check if directory exists
                if (!Directory.Exists(path))
                {
                    // Create it
                    Directory.CreateDirectory(path);
                }

                // Double check if it really exists now
                if (Directory.Exists(path))
                {
                    // Check if we have read & write permissions
                    var permissionSet = new PermissionSet(PermissionState.None);
                    var readWritePermission = new FileIOPermission(FileIOPermissionAccess.Read & FileIOPermissionAccess.Write, path);
                    permissionSet.AddPermission(readWritePermission);
                    
                    if (!permissionSet.IsSubsetOf(AppDomain.CurrentDomain.PermissionSet))
                    {
                        this.errorMessage += String.Format("No write permissions on directory '{0}'.", path);
                    }
                }
                else
                {
                    this.errorMessage += String.Format("Could not create directory '{0}'.", path);
                }
            }
            catch (Exception ex)
            {
                this.errorMessage += String.Format("No write permissions on directory '{0}' ({1}.", path, ex.Message);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the error message if verification failed
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
            set
            {
                this.errorMessage = value;
            }
        }

        #endregion
    }
}
