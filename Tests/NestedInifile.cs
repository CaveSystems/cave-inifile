using Cave;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;
using Test;

namespace Tests
{
    [TestFixture]
    public class NestedInifile
    {
        public class NestedRoot
        {
            public static NestedRoot Random(CultureInfo culture)
            {
                var result = new NestedRoot();
                result.Sub1.Add(SettingsObjectFields.Random(culture));
                result.Sub1.Add(SettingsObjectFields.Random(culture));
                result.Sub2.Add("test1", SettingsStructFields.Random(culture));
                result.Sub2.Add("test2", SettingsStructFields.Random(culture));
                result.Sub3 = SettingsObjectProperties.Random(culture);
                result.Sub4 = SettingsStructProperties.Random(culture);
                return result;
            }

            [IniSection]
            public IList<SettingsObjectFields> Sub1 { get; set; } = new List<SettingsObjectFields>();

            [IniSection(Name = "Section2")]
            public IDictionary<string, SettingsStructFields> Sub2 { get; set; } = new Dictionary<string, SettingsStructFields>();

            [IniSection(SettingsType = IniSettingsType.Properties)]
            public SettingsObjectProperties Sub3 { get; set; }

            [IniSection(Name = "Section4", SettingsType = IniSettingsType.Properties)]
            public SettingsStructProperties Sub4 { get; set; }

            [IniIgnore]
            public object SomethingSpecial { get; set; } = new Random();
        }

        [Test]
        public void Test()
        {
            var writer = new IniWriter();
            var test = NestedRoot.Random(CultureInfo.InvariantCulture);
            writer.WriteProperties("test", test);

            var reader = writer.ToReader();
            var current = reader.ReadObjectProperties<NestedRoot>("test");
            Assert.AreEqual(test, current);
        }
    }
}
