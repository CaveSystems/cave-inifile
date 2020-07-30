using System;

namespace Cave
{
    /// <summary>
    /// Attribute for marking fields or properties for serialization into ini file sections.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class IniSectionAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IniSectionAttribute"/> class.
        /// </summary>
        /// <param name="section"></param>
        public IniSectionAttribute(string section) => Section = section ?? throw new ArgumentNullException(nameof(section));

        /// <summary>
        /// Gets the section name.
        /// </summary>
        public string Section { get; }

        /// <summary>
        /// Gets or sets the type of elements to be serialized.
        /// </summary>
        public IniSettingsType SettingsType { get; set; }
    }
}
