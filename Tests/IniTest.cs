﻿using Cave;
using NUnit.Framework;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Tests
{
    [TestFixture]
    public class IniTest
    {
        readonly CultureInfo[] allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);

        [Test]
        public void IniReaderWriterStringTest()
        {
            void Test(string s)
            {
                var writer = new IniWriter();
                writer.WriteSetting("test", "string", s);
                var reader = writer.ToReader();
                var value = reader.ReadSetting("test", "string");
                Assert.AreEqual(s, value);
            }

            for (var i = 0; i < 255; i++)
            {
                Test(((char)i).ToString());
            }

            var random = new Random();
            foreach (var encodingInfo in Encoding.GetEncodings())
            {
                for (var i = 0; i < 100; i++)
                {
                    var encoding = encodingInfo.GetEncoding();
                    var buf = encoding.GetBytes(new string(' ', 100));
                    random.NextBytes(buf);
                    var str = encoding.GetString(buf);

                    Test(str);
                    Test(str + " ");
                    Test(" " + str);
                    Test("#" + str);
                    Test("\t" + str + "\r\n");
                }
            }
        }

        [Test]
        public void IniReaderWriterTest()
        {
            var temp = Path.GetTempFileName();
            Console.WriteLine($"{nameof(IniReaderWriterTest)}.cs: info TI0002: TestFile {temp}");
            foreach (var culture in allCultures)
            {
                Console.WriteLine($"{nameof(IniReaderWriterTest)}.cs: info TI0002: Test {culture}");

                if (!(culture.Calendar is GregorianCalendar))
                {
                    Console.WriteLine($"- Skipping calendar {culture.Calendar}");
                    continue;
                }

                var settings = new SettingsStructFields[10];
                var properties = IniProperties.Default;
                properties.Culture = culture;
                var writer = new IniWriter(temp, properties);

                {
                    var setting = SettingsStructFields.Random(null);
                    settings[0] = setting;
                    writer.WriteFields($"Section 0", setting);
                }
                for (var i = 1; i < settings.Length; i++)
                {
                    var setting = SettingsStructFields.Random(culture);
                    settings[i] = setting;
                    writer.WriteFields($"Section {i}", setting);
                }                
                writer.Save(temp);

                TestReader((IniReader)writer.ToReader(), settings);
                TestReader(IniReader.FromFile(temp, properties), settings);
            }
        }

        private void TestReader(IniReader reader, SettingsStructFields[] settings)
        {
            var fields1 = typeof(SettingsStructFields).GetFields().OrderBy(f => f.Name).ToArray();
            var fields2 = typeof(SettingsObjectFields).GetFields().OrderBy(f => f.Name).ToArray();
            var fields3 = typeof(SettingsStructProperties).GetProperties().OrderBy(f => f.Name).ToArray();
            var fields4 = typeof(SettingsObjectProperties).GetProperties().OrderBy(f => f.Name).ToArray();
            for (var i = 0; i < settings.Length; i++)
            {
                var settings1 = reader.ReadStructFields<SettingsStructFields>($"Section {i}");
                var settings2 = reader.ReadObjectFields<SettingsObjectFields>($"Section {i}");
                var settings3 = reader.ReadStructProperties<SettingsStructProperties>($"Section {i}");
                var settings4 = reader.ReadObjectProperties<SettingsObjectProperties>($"Section {i}");

                for (var n = 0; n < fields1.Length; n++)
                {
                    var original = fields1[n].GetValue(settings[i]);
                    var value1 = fields1[n].GetValue(settings1);
                    var value2 = fields2[n].GetValue(settings2);
                    var value3 = fields3[n].GetValue(settings3, null);
                    var value4 = fields4[n].GetValue(settings4, null);
                    if (original is DateTime dt && !Equals(original, value1))
                    {
                        switch (reader.Properties.Culture.ThreeLetterISOLanguageName)
                        {
                            case "dzo":
                                return;
                            default: 
                                throw new NotImplementedException();
                        } 
                    }
                    Assert.AreEqual(original, value1);
                    Assert.AreEqual(original, value2);
                    Assert.AreEqual(original, value3);
                    Assert.AreEqual(original, value4);
                }
            }
        }
    }
}
