using System;
using System.Globalization;
using System.Text;

namespace Tests
{
    public struct SettingsStructProperties : IEquatable<SettingsStructProperties>
    {
        public string SampleString { get; set; }
        public sbyte SampleInt8 { get; set; }
        public short SampleInt16 { get; set; }
        public int SampleInt32 { get; set; }
        public long SampleInt64 { get; set; }
        public byte SampleUInt8 { get; set; }
        public ushort SampleUInt16 { get; set; }
        public uint SampleUInt32 { get; set; }
        public ulong SampleUInt64 { get; set; }
        public float SampleFloat { get; set; }
        public double SampleDouble { get; set; }
        public decimal SampleDecimal { get; set; }
        public bool SampleBool { get; set; }
        public DateTime SampleDateTime { get; set; }
        public TimeSpan SampleTimeSpan { get; set; }
        public SettingEnum SampleEnum { get; set; }
        public SettingFlagEnum SampleFlagEnum { get; set; }
        public uint? SampleNullableUInt32 { get; set; }

        static readonly Random random = new Random(Environment.TickCount);

        public static SettingsStructProperties Random(CultureInfo culture)
        {
            var len = random.Next(0, 90);
            char[] str;
            if (culture == null)
            {
                var buf = new byte[len * 2];
                random.NextBytes(buf);
                str = Encoding.Unicode.GetString(buf).ToCharArray();
            }
            else
            {
                var encoding = Encoding.GetEncoding(culture.TextInfo.ANSICodePage);
                var buf = encoding.GetBytes(new string(' ', len));
                random.NextBytes(buf);
                str = encoding.GetString(buf).ToCharArray();
            }

            var dateTime = DateTime.Today.AddSeconds(random.Next(1, 60 * 60 * 24));
            return new SettingsStructProperties()
            {
                SampleString = new string(str),
                SampleBool = random.Next(1, 100) < 51,
                SampleDateTime = dateTime,
                SampleTimeSpan = TimeSpan.FromSeconds(random.NextDouble()),
                SampleDecimal = (decimal)random.NextDouble() / (decimal)random.NextDouble(),
                SampleDouble = random.NextDouble(),
                SampleEnum = (SettingEnum)random.Next(0, 9),
                SampleFlagEnum = (SettingFlagEnum)random.Next(0, 1 << 10),
                SampleFloat = (float)random.NextDouble(),
                SampleInt16 = (short)random.Next(short.MinValue, short.MaxValue),
                SampleInt32 = random.Next() - random.Next(),
                SampleInt64 = ((long)random.Next() - (long)random.Next()) * (long)random.Next(),
                SampleInt8 = (sbyte)random.Next(sbyte.MinValue, sbyte.MaxValue),
                SampleUInt8 = (byte)random.Next(byte.MinValue, byte.MaxValue),
                SampleUInt16 = (ushort)random.Next(ushort.MinValue, ushort.MaxValue),
                SampleUInt32 = (uint)random.Next() + (uint)random.Next(),
                SampleUInt64 = ((ulong)random.Next() + (ulong)random.Next()) * (ulong)random.Next(),
                SampleNullableUInt32 = random.Next(1, 100) < 20 ? null : (uint?)random.Next(),
            };
        }

        public bool Equals(SettingsStructProperties other) => SampleString == other.SampleString &&
                                                              SampleInt8 == other.SampleInt8 &&
                                                              SampleInt16 == other.SampleInt16 &&
                                                              SampleInt32 == other.SampleInt32 &&
                                                              SampleInt64 == other.SampleInt64 &&
                                                              SampleUInt8 == other.SampleUInt8 &&
                                                              SampleUInt16 == other.SampleUInt16 &&
                                                              SampleUInt32 == other.SampleUInt32 &&
                                                              SampleUInt64 == other.SampleUInt64 &&
                                                              SampleFloat.Equals(other.SampleFloat) &&
                                                              SampleDouble.Equals(other.SampleDouble) &&
                                                              SampleDecimal == other.SampleDecimal &&
                                                              SampleBool == other.SampleBool &&
                                                              SampleDateTime.Equals(other.SampleDateTime) &&
                                                              SampleTimeSpan.Equals(other.SampleTimeSpan) &&
                                                              SampleEnum == other.SampleEnum &&
                                                              SampleFlagEnum == other.SampleFlagEnum &&
                                                              SampleNullableUInt32 == other.SampleNullableUInt32;

        public override bool Equals(object obj) => obj is SettingsStructProperties other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (SampleString != null ? SampleString.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ SampleInt8.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleInt16.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleInt32;
                hashCode = (hashCode * 397) ^ SampleInt64.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleUInt8.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleUInt16.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)SampleUInt32;
                hashCode = (hashCode * 397) ^ SampleUInt64.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleFloat.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleDouble.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleDecimal.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleBool.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleDateTime.GetHashCode();
                hashCode = (hashCode * 397) ^ SampleTimeSpan.GetHashCode();
                hashCode = (hashCode * 397) ^ (int)SampleEnum;
                hashCode = (hashCode * 397) ^ (int)SampleFlagEnum;
                hashCode = (hashCode * 397) ^ SampleNullableUInt32.GetHashCode();
                return hashCode;
            }
        }
    }
}
