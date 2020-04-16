using Cave;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestFixture]
    public class TestInifile
    {
        [Test]
        public void Test()
        {
            IniWriter writer = new IniWriter();
            for (int i = 0; i < 100; i++)
            {
                var s = TestStruct.Create(i ^ (1 << i % 32));
                writer.WriteFields($"struct{i}", s);
            }
            var reader = writer.ToReader();
            for (int i = 0; i < 100; i++)
            {
                var expected = TestStruct.Create(i ^ (1 << i % 32));
                var current = reader.ReadStructFields<TestStruct>($"struct{i}");
                Assert.AreEqual(expected, current);
            }
        }
    }
}
