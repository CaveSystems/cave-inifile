using System;

namespace Cave
{
    /// <summary>
    /// Provides available types of ini settings serialization.
    /// </summary>
    [Obsolete("Update to nuget package Cave.IO!")]
    public enum IniSettingsType
    {
        /// <summary>
        /// Read / write fields of a struct / class
        /// </summary>
        Fields = 0,

        /// <summary>
        /// Read / write properties of a struct / class
        /// </summary>
        Properties = 1,
    }
}
