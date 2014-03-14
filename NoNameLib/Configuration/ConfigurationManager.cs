using System;

namespace NoNameLib.Configuration
{
    /// <summary>
    /// Static wrapper class for Global.ConfigurationProvider.
    /// </summary>
    public static class ConfigurationManager
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static bool GetBool(string name)
        {
            return Global.ConfigurationProvider.GetBool(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static bool GetBool(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetBool(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static bool GetBoolRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetBoolRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static bool GetBoolRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetBoolRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static byte GetByte(string name)
        {
            return Global.ConfigurationProvider.GetByte(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static byte GetByte(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetByte(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static byte GetByteRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetByteRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static byte GetByteRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetByteRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static char GetChar(string name)
        {
            return Global.ConfigurationProvider.GetChar(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static char GetChar(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetChar(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static char GetCharRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetCharRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static char GetCharRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetCharRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static DateTime GetDateTime(string name)
        {
            return Global.ConfigurationProvider.GetDateTime(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static DateTime GetDateTime(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetDateTime(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static DateTime GetDateTimeRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetDateTimeRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static DateTime GetDateTimeRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetDateTimeRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static decimal GetDecimal(string name)
        {
            return Global.ConfigurationProvider.GetDecimal(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static decimal GetDecimal(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetDecimal(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static decimal GetDecimalRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetDecimalRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static decimal GetDecimalRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetDecimalRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static double GetDouble(string name)
        {
            return Global.ConfigurationProvider.GetDouble(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static double GetDouble(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetDouble(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static double GetDoubleRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetDoubleRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static double GetDoubleRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetDoubleRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static float GetFloat(string name)
        {
            return Global.ConfigurationProvider.GetFloat(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static float GetFloat(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetFloat(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static float GetFloatRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetFloatRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static float GetFloatRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetFloatRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static int GetInt(string name)
        {
            return Global.ConfigurationProvider.GetInt(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static int GetInt(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetInt(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static int GetIntRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetIntRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static int GetIntRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetIntRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static long GetLong(string name)
        {
            return Global.ConfigurationProvider.GetLong(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static long GetLong(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetLong(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static long GetLongRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetLongRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static long GetLongRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetLongRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static short GetShort(string name)
        {
            return Global.ConfigurationProvider.GetShort(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static short GetShort(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetShort(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static short GetShortRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetShortRefreshed(name);
        }

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static short GetShortRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetShortRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static string GetString(string name)
        {
            return Global.ConfigurationProvider.GetString(name);
        }

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        public static string GetString(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetString(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.String instance containing the current value</returns>
        public static string GetStringRefreshed(string name)
        {
            return Global.ConfigurationProvider.GetStringRefreshed(name);
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Boolean instance containing the current value</returns>
        public static string GetStringRefreshed(string name, bool applicationSpecific)
        {
            return Global.ConfigurationProvider.GetStringRefreshed(name, applicationSpecific);
        }

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.String instance containing the current value</returns>
        public static string GetPassword(string name, bool applicationSpecific = false)
        {
            return Global.ConfigurationProvider.GetPassword(name, applicationSpecific);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        public static void SetValue(string name, object value)
        {
            Global.ConfigurationProvider.SetValue(name, value);
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific. If null, uses the default value for the configuration item.</param>
        public static void SetValue(string name, object value, bool applicationSpecific)
        {
            Global.ConfigurationProvider.SetValue(name, value, applicationSpecific);
        }

        /// <summary>
        /// Sets the password.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific. If null, uses the default value for the configuration item.</param>
        public static void SetPassword(string name, string value, bool applicationSpecific = false)
        {
            Global.ConfigurationProvider.SetPassword(name, value, applicationSpecific);
        }

        /// <summary>
        /// Get's if the setting is defined for the current running application.
        /// This might not be the case when a certain set of ConfigInfo is not initialized in the application start.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>
        /// bool
        /// </returns>
        public static bool IsDefined(string name)
        {
            return Global.ConfigurationProvider.IsDefined(name);
        }
    }
}
