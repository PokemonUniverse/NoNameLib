using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NoNameLib.Configuration
{
    /// <summary>
    /// Collection class used for storing Dionysos.Configuration.ConfigurationItemCollection instances in
    /// </summary>
    public class ConfigurationInfoCollection : ICollection<ConfigurationItemCollection>
    {
        #region Fields

        private ArrayList items;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the ConfigurationInfoCollection class
        /// </summary>	
        public ConfigurationInfoCollection()
        {
            this.items = new ArrayList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an Dionysos.Configuration.ConfigurationItemCollection instance to the collection
        /// </summary>
        /// <param name="configurationItemCollection">The Dionysos.Configuration.ConfigurationItemCollection instance to add to the collection</param>
        public void Add(ConfigurationItemCollection configurationItemCollection)
        {
            this.items.Add(configurationItemCollection);
        }

        /// <summary>
        /// Clears all the items in the collection
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Checks whether the specified Dionysos.Configuration.ConfigurationItemCollection instance is already in the collection
        /// </summary>
        /// <param name="configurationItemCollection">The Dionysos.Configuration.ConfigurationItemCollection instance to check</param>
        /// <returns>True if the Dionysos.Configuration.ConfigurationItemCollection instance is in the collection, False if not</returns>
        public bool Contains(ConfigurationItemCollection configurationItemCollection)
        {
            return this.items.Cast<object>().Any(t => (t as ConfigurationItemCollection) == configurationItemCollection);
        }

        /// <summary>
        /// Copies the items from this collection to an array at the specified index
        /// </summary>
        /// <param name="array">The array to copy the items to</param>
        /// <param name="index">The index to copy the items at</param>
        public void CopyTo(ConfigurationItemCollection[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        /// <summary>
        /// Removes the specified Dionysos.Configuration.ConfigurationItemCollection instance from this collection
        /// </summary>
        /// <param name="configurationItemCollection">The Dionysos.Configuration.ConfigurationItemCollection instance to remove</param>
        public bool Remove(ConfigurationItemCollection configurationItemCollection)
        {
            this.items.Remove(configurationItemCollection);
            return true;
        }

        /// <summary>
        /// Returns an enumerator for a range of elements in the Lynx-media.Web.Collections.ConfigurationInfoCollection
        /// </summary>
        /// <returns>The enumerator instance</returns>
        public IEnumerator<ConfigurationItemCollection> GetEnumerator()
        {
            return this.items.Cast<ConfigurationItemCollection>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator for a range of elements in the Lynx-media.Web.Collections.ConfigurationInfoCollection
        /// </summary>
        /// <returns>The enumerator instance</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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
        /// Gets an Dionysos.Configuration.ConfigurationItemCollection instance from the collection from the specified index
        /// </summary>
        /// <param name="index">The index of the Dionysos.Configuration.ConfigurationItemCollection instance to get</param>
        /// <returns>An Dionysos.Configuration.ConfigurationItemCollection instance</returns>
        public ConfigurationItemCollection this[int index]
        {
            get
            {
                return this.items[index] as ConfigurationItemCollection;
            }
        }

        #endregion
    }
}
