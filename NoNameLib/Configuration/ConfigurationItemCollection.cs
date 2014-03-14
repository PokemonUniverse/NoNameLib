using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NoNameLib.Configuration
{
    /// <summary>
    /// Collection class used for storing Dionysos.Configuration.ApplicationSettingsItem instances in
    /// </summary>
    public class ConfigurationItemCollection : ICollection<ConfigurationItem>
    {
        #region Fields

        private ArrayList items;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the ApplicationSettingsItemCollection class
        /// </summary>	
        public ConfigurationItemCollection()
        {
            this.items = new ArrayList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds an Dionysos.Configuration.ApplicationSettingsItem instance to the collection
        /// </summary>
        /// <param name="section">The Dionysos.Configuration.ApplicationSettingsItem instance to add to the collection</param>
        public void Add(ConfigurationItem section)
        {
            this.items.Add(section);
        }

        /// <summary>
        /// Clears all the items in the collection
        /// </summary>
        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Checks whether the specified Dionysos.Configuration.ApplicationSettingsItem instance is already in the collection
        /// </summary>
        /// <param name="section">The Dionysos.Configuration.ApplicationSettingsItem instance to check</param>
        /// <returns>True if the Dionysos.Configuration.ApplicationSettingsItem instance is in the collection, False if not</returns>
        public bool Contains(ConfigurationItem section)
        {
            bool contains = false;

            for (int i = 0; i < this.items.Count; i++)
            {
                if ((this.items[i] as ConfigurationItem) == section)
                {
                    contains = true;
                }
            }

            return contains;
        }

        /// <summary>
        /// Copies the items from this collection to an array at the specified index
        /// </summary>
        /// <param name="array">The array to copy the items to</param>
        /// <param name="index">The index to copy the items at</param>
        public void CopyTo(ConfigurationItem[] array, int index)
        {
            this.items.CopyTo(array, index);
        }

        /// <summary>
        /// Removes the specified Dionysos.Configuration.ApplicationSettingsItem instance from this collection
        /// </summary>
        /// <param name="section">The Dionysos.Configuration.ApplicationSettingsItem instance to remove</param>
        public bool Remove(ConfigurationItem section)
        {
            this.items.Remove(section);
            return true;
        }

        /// <summary>
        /// Returns an enumerator for a range of elements in the Dionysos.Windows.Forms.Collections.ControlCollection
        /// </summary>
        /// <returns>The enumerator instance</returns>
        public IEnumerator<ConfigurationItem> GetEnumerator()
        {
            return this.items.Cast<ConfigurationItem>().GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator for a range of elements in the Dionysos.Windows.Forms.Collections.ControlCollection
        /// </summary>
        /// <returns>The enumerator instance</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private ConfigurationItem GetApplicationSettingsItem(string itemName)
        {
            ConfigurationItem item = null;

            for (int i = 0; i < this.items.Count; i++)
            {
                var temp = this.items[i] as ConfigurationItem;
                if (temp != null && temp.Name == itemName)
                {
                    item = temp;
                    break;
                }
            }

            /*if (item == null)
            {
                // No item found, so create a new one
                item = new ConfigurationItem(itemName);
                this.items.Add(item);
            }*/

            return item;
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
        /// Gets an Dionysos.Configuration.ApplicationSettingsItem instance from the collection from the specified index
        /// </summary>
        /// <param name="index">The index of the Dionysos.Configuration.ApplicationSettingsItem instance to get</param>
        /// <returns>An Dionysos.Configuration.ApplicationSettingsItem instance</returns>
        public ConfigurationItem this[int index]
        {
            get
            {
                return this.items[index] as ConfigurationItem;
            }
        }

        /// <summary>
        /// Gets an Dionysos.Configuration.ApplicationSettingsItem instance from the collection with the specified name
        /// </summary>
        /// <param name="name">The index of the Dionysos.Configuration.ApplicationSettingsItem instance to get</param>
        /// <returns>An Dionysos.Configuration.ApplicationSettingsItem instance</returns>
        public ConfigurationItem this[string name]
        {
            get
            {
                return this.GetApplicationSettingsItem(name);
            }
        }

        #endregion
    }
}
