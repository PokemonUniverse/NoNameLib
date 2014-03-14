using NoNameLib.Attributes;

namespace NoNameLib.Enums
{
    /// <summary>
    /// Logging level
    /// </summary>
    public enum LoggingLevel
    {
        [StringValue("Debug")]
        Debug = 400,

        [StringValue("Verbose")]
        Verbose = 300,

        [StringValue("Info")]
        Info = 200,

        [StringValue("Warning")]
        Warning = 100,

        [StringValue("Error")]
        Error = 0
    }
}
