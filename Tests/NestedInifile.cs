using Cave;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Tests
{
    [TestFixture]
    public class NestedInifile
    {
        public class NestedRoot : IEquatable<NestedRoot>
        {
            public static NestedRoot Random(CultureInfo culture)
            {
                var result = new NestedRoot();
                result.Sub1 = SettingsObjectFields.Random(culture);
                result.Sub2 = SettingsStructFields.Random(culture);
                result.Sub3 = SettingsObjectProperties.Random(culture);
                result.Sub4 = SettingsStructProperties.Random(culture);
                result.SomethingSpecial = new Random().NextDouble();
                return result;
            }

            public bool Equals(NestedRoot other) => 
                Equals(Sub1, other.Sub1) &&
                Equals(Sub2, other.Sub2) && 
                Equals(Sub3, other.Sub3) && 
                Equals(Sub4, other.Sub4);

            [IniSection]
            public SettingsObjectFields Sub1 { get; set; }

            [IniSection(Name = "Section2")]
            public SettingsStructFields Sub2 { get; set; }

            [IniSection(SettingsType = IniSettingsType.Properties)]
            public SettingsObjectProperties Sub3 { get; set; }

            [IniSection(Name = "Section4", SettingsType = IniSettingsType.Properties)]
            public SettingsStructProperties Sub4 { get; set; }

            [IniIgnore]
            public object SomethingSpecial { get; set; }

            public override bool Equals(object obj)
            {
                return Equals(obj as NestedRoot);
            }
        }

        [Test]
        public void NestedInifileTest()
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
