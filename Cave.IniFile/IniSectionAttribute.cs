using System;

namespace Cave
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class IniSectionAttribute : Attribute
    {
        public IniSectionAttribute(string section) => Section = section;

        public string Section { get; }

        public bool RemoveComments { get; set; }
    }
}
