using System;

namespace Cave
{
    /// <summary>
    /// Attribute for marking fields or properties to be ignored by ini file serialization.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    [Obsolete("Update to nuget package Cave.IO!")]
    public class IniIgnoreAttribute : Attribute
    {
    }
}
