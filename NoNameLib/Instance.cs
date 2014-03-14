using System;

namespace NoNameLib
{
    /// <summary>
    /// Static class  for checking if value are empty or not
    /// </summary>
    public class Instance
    {
        #region Fields

        static Instance instance = null;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs an instance of the Instance class if the instance has not been initialized yet
        /// </summary>
        public Instance()
        {
            if (instance == null)
            {
                // first create the instance
                instance = new Instance();
                // Then init the instance (the init needs the instance to fill it)
                instance.Init();
            }
        }

        #endregion

        #region Methods

        private void Init()
        {
        }

        /// <summary>
        /// Checks if an argument is empty
        /// </summary>
        /// <param name="argument">The argument to check for emptiness</param>
        /// <param name="argumentName">The name of the argument</param>
        /// <returns>An ArgumentNullException if the argument is null, otherwise False</returns>
        public static bool ArgumentIsEmpty(object argument, string argumentName)
        {
            bool isEmpty = Empty(argument);

			// This is never called when a exception occurs, so the result is always false:)
			// this method should be called without checking the return value.
			// when the caller passed the method we know that the arugument is filled
            return isEmpty;
        }

        #region Empty methods

        /// <summary>
        /// Checks if an instance of an object is empty
        /// </summary>
        /// <param name="instance">The instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(object instance)
        {
            bool isEmpty = false;

            //if (instance is string && ((string)instance).Length == 0)
            //    isEmpty = true;
            //else
                isEmpty = (instance == null);

            return isEmpty;
        }

        /// <summary>
        /// Checks if an instance of a System.Boolean is empty
        /// </summary>
        /// <param name="instance">The System.Boolean instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(bool instance)
        {
            return (instance == false);
        }

        /// <summary>
        /// Checks if an instance of a System.Byte is empty
        /// </summary>
        /// <param name="instance">The System.Byte instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(byte instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of a System.SByte is empty
        /// </summary>
        /// <param name="instance">The System.SByte instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(sbyte instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of a System.Char is empty
        /// </summary>
        /// <param name="instance">The System.Char instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(char instance)
        {
            return (instance == '\0');
        }

        /// <summary>
        /// Checks if an instance of a System.Decimal is empty
        /// </summary>
        /// <param name="instance">The System.Decimal instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(decimal instance)
        {
            return (instance == 0.0m);
        }

        /// <summary>
        /// Checks if an instance of a System.Double is empty
        /// </summary>
        /// <param name="instance">The System.Double instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(double instance)
        {
            return (instance == 0.0d);
        }

        /// <summary>
        /// Checks if an instance of a System.Single is empty
        /// </summary>
        /// <param name="instance">The System.Single instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(float instance)
        {
            return (instance == 0.0f);
        }

        /// <summary>
        /// Checks if an instance of a System.Int32 is empty
        /// </summary>
        /// <param name="instance">The System.Int32 instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(int instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of a System.UInt32 is empty
        /// </summary>
        /// <param name="instance">The System.UInt32 instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(uint instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of a System.Int64 is empty
        /// </summary>
        /// <param name="instance">The System.Int64 instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(long instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of a System.UInt64 is empty
        /// </summary>
        /// <param name="instance">The System.UInt64 instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(ulong instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of a System.Int16 is empty
        /// </summary>
        /// <param name="instance">The System.Int16 instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(short instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of a System.UInt16 is empty
        /// </summary>
        /// <param name="instance">The System.UInt16 instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(ushort instance)
        {
            return (instance == 0);
        }

        /// <summary>
        /// Checks if an instance of an System.String is empty
        /// </summary>
        /// <param name="instance">The System.String instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(string instance)
        {
            return string.IsNullOrEmpty(instance);
        }

        /// <summary>
        /// Checks if an instance of an System.Array is empty
        /// </summary>
        /// <param name="instance">The System.Array instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(Array instance)
        {
            return (instance == null || instance.Length == 0);
        }

        /// <summary>
        /// Checks if an instance of an System.Datetime is empty
        /// </summary>
        /// <param name="instance">The System.DateTime instance to check for emptiness</param>
        /// <returns>True if the instance is empty, False is not</returns>
        public static bool Empty(DateTime instance)
        {
            return (instance == null || instance == DateTime.MinValue || instance.ToShortDateString() == "1-1-0001");
        }

        #endregion

        /// <summary>
        /// Sets an instance to null, so it becomes available for garbage collection
        /// </summary>
        /// <param name="instance">The instance to set to null</param>
        public static void SetToNull(ref object instance)
        {
            instance = null;
        }

        #endregion
    }
}
