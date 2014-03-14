using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NoNameLib.Interfaces
{
    /// <summary>
    /// Interface used for implementing configuration management
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        bool GetBool(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        bool GetBool(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        bool GetBoolRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        bool GetBoolRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        byte GetByte(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        byte GetByte(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        byte GetByteRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        byte GetByteRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        char GetChar(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        char GetChar(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        char GetCharRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        char GetCharRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        DateTime GetDateTime(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        DateTime GetDateTime(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        DateTime GetDateTimeRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        DateTime GetDateTimeRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        decimal GetDecimal(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        decimal GetDecimal(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        decimal GetDecimalRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        decimal GetDecimalRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        double GetDouble(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        double GetDouble(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        double GetDoubleRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        double GetDoubleRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        float GetFloat(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        float GetFloat(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        float GetFloatRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        float GetFloatRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        int GetInt(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        int GetInt(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        int GetIntRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        int GetIntRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        long GetLong(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        long GetLong(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        long GetLongRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        long GetLongRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        short GetShort(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        short GetShort(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        short GetShortRefreshed(string name);

        /// <summary>
        /// Gets the value after is has been refreshed by a re-read from the persistent storage.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        short GetShortRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        string GetString(string name);

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        string GetString(string name, bool applicationSpecific);

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <returns>An System.String instance containing the current value</returns>
        string GetStringRefreshed(string name);

        /// <summary>
        /// Gets the current value of this configuration item
        /// </summary>
        /// <param name="name">The name of the configuration item</param>
        /// <param name="applicationSpecific">The flag which indicates whether the configuration value is application specific</param>
        /// <returns>An System.Boolean instance containing the current value</returns>
        string GetStringRefreshed(string name, bool applicationSpecific);

        /// <summary>
        /// Gets a password
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        string GetPassword(string name);

        /// <summary>
        /// Gets a password
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific.</param>
        /// <returns>Returns the value if found and type matches; otherwise returns default value defined for configuration item.</returns>
        string GetPassword(string name, bool applicationSpecific);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        void SetValue(string name, object value);

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific. If null, uses the default value for the configuration item.</param>
        void SetValue(string name, object value, bool applicationSpecific);

        /// <summary>
        /// Sets a password value
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        void SetPassword(string name, string value);

        /// <summary>
        /// Sets a password value
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <param name="value">The new value for the configuration item.</param>
        /// <param name="applicationSpecific">Indicates whether the configuration item is application specific. If null, uses the default value for the configuration item.</param>
        void SetPassword(string name, string value, bool applicationSpecific);

        /// <summary>
        /// Get's if the setting is defined for the current running application.
        /// This might not be the case when a certain set of ConfigInfo is not initialized in the application start.
        /// </summary>
        /// <param name="name">The name of the configuration value.</param>
        /// <returns>bool</returns>
        bool IsDefined(string name);
    }
}
