using System.Collections;
using System.Collections.Generic;
using NoNameLib.Exceptions;
using NoNameLib.Interfaces;

namespace NoNameLib.Verification
{
    /// <summary>
    /// Collection class used for storing Dionysos.Interfaces.IVerifier instances in
    /// </summary>
    public class VerifierCollection : ICollection<IVerifier>
    {
        #region Fields

        private readonly ArrayList items;
        private string errorMessage = string.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the VerifierCollection class
        /// </summary>	
        public VerifierCollection()
        {
            this.items = new ArrayList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an Dionysos.Interfaces.IVerifier instance to the collection
        /// </summary>
        /// <param name="verifier">The Dionysos.Interfaces.IVerifier instance to add to the collection</param>
        public void Add(IVerifier verifier)
        {
            this.items.Add(verifier);
        }

        /// <summary>
        /// Clears all the items in the collection
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Checks whether the specified Dionysos.Interfaces.IVerifier instance is already in the collection
        /// </summary>
        /// <param name="verifier">The Dionysos.Interfaces.IVerifier instance to check</param>
        /// <returns>True if the Dionysos.Interfaces.IVerifier instance is in the collection, False if not</returns>
        public bool Contains(IVerifier verifier)
        {
            bool contains = false;

            for (int i = 0; i < this.items.Count; i++)
            {
                if ((this.items[i] as IVerifier) == verifier)
                {
                    contains = true;
                    break;
                }
            }

            return contains;
        }

        /// <summary>
        /// Copies the items from this collection to an array at the specified index
        /// </summary>
        /// <param name="array">The array to copy the items to</param>
        /// <param name="index">The index to copy the items at</param>
        public void CopyTo(IVerifier[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        /// <summary>
        /// Removes the specified Dionysos.Interfaces.IVerifier instance from this collection
        /// </summary>
        /// <param name="verifier">The Dionysos.Interfaces.IVerifier instance to remove</param>
        public bool Remove(IVerifier verifier)
        {
            this.items.Remove(verifier);
            return true;
        }

        /// <summary>
        /// Returns an enumerator for a range of elements in the Lynx-media.Web.Collections.VerifierCollection
        /// </summary>
        /// <returns>The enumerator instance</returns>
        public IEnumerator<IVerifier> GetEnumerator()
        {
            return (IEnumerator<IVerifier>)this.items.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator for a range of elements in the Lynx-media.Web.Collections.VerifierCollection
        /// </summary>
        /// <returns>The enumerator instance</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Verifies the IVerifier instances in the collection
        /// </summary>
        /// <returns>True if verification was succesful, False if not</returns>
        public bool Verify()
        {
            this.errorMessage = string.Empty;
            bool succes = true;

            for (int i = 0; i < this.items.Count; i++)
            {
                IVerifier verifier = this[i];
                if (Instance.Empty(verifier))
                {
                    throw new EmptyException("Variable 'verifier' is empty.");
                }
                
                if (!verifier.Verify())
                {
                    succes = false;

                    if (errorMessage != string.Empty)
                        errorMessage += "\r\n";

                    errorMessage += verifier.ErrorMessage;
                }
            }

            return succes;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the amount of items in this collection
        /// </summary>
        public int Count
        {
            get
            {
                return this.items.Count;
            }
        }

        /// <summary>
        /// Boolean value indicating whether this collection is read only or not
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return this.items.IsReadOnly;
            }
        }

        /// <summary>
        /// Gets an Dionysos.Interfaces.IVerifier instance from the collection from the specified index
        /// </summary>
        /// <param name="index">The index of the Dionysos.Interfaces.IVerifier instance to get</param>
        /// <returns>An Dionysos.Interfaces.IVerifier instance</returns>
        public IVerifier this[int index]
        {
            get
            {
                return this.items[index] as IVerifier;
            }
        }

        /// <summary>
        /// Gets the error message if verification has failed
        /// </summary>
        public string ErrorMessage
        {
            get
            {
                return this.errorMessage;
            }
        }

        #endregion
    }
}
