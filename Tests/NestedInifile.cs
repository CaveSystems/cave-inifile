using Cave;
using NUnit.Framework;
using System.Collections.Generic;
using Test;

namespace Tests
{
    //[TestFixture]
    public class NestedInifile
    {
        public class NestedRoot
        {
            public IList<SettingsObjectFields> Sub1 { get; set; } = new List<SettingsObjectFields>();

            public IDictionary<string, SettingsStructFields> Sub2 { get; set; } = new Dictionary<string, SettingsStructFields>();
        }

        //[Test]
        public void Test()
        {
            IniWriter writer = new IniWriter();
            var test = new NestedRoot();
            writer.WriteProperties("test", test);

            var reader = writer.ToReader();
            var current = reader.ReadObjectProperties<NestedRoot>("test");
            Assert.AreEqual(test, current);
        }
    }
}
