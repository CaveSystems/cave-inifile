using System;
using System.Globalization;
using System.Text;

namespace Tests
{
    public class SettingsObjectFields : IEquatable<SettingsObjectFields>
    {
        public string SampleString;
        public sbyte SampleInt8;
        public short SampleInt16;
        public int SampleInt32;
        public long SampleInt64;
        public byte SampleUInt8;
        public ushort SampleUInt16;
        public uint SampleUInt32;
        public ulong SampleUInt64;
        public float SampleFloat;
        public double SampleDouble;
        public decimal SampleDecimal;
        public bool SampleBool;
        public DateTime SampleDateTime;
        public TimeSpan SampleTimeSpan;
        public SettingEnum SampleEnum;
        public SettingFlagEnum SampleFlagEnum;
        public uint? SampleNullableUInt32;

        static readonly Random random = new Random(Environment.TickCount);

        public static SettingsObjectFields Random(CultureInfo culture)
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
            return new SettingsObjectFields()
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

        public bool Equals(SettingsObjectFields other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return SampleString == other.SampleString && SampleInt8 == other.SampleInt8 &&
                   SampleInt16 == other.SampleInt16 && SampleInt32 == other.SampleInt32 &&
                   SampleInt64 == other.SampleInt64 && SampleUInt8 == other.SampleUInt8 &&
                   SampleUInt16 == other.SampleUInt16 && SampleUInt32 == other.SampleUInt32 &&
                   SampleUInt64 == other.SampleUInt64 && SampleFloat.Equals(other.SampleFloat) &&
                   SampleDouble.Equals(other.SampleDouble) && SampleDecimal == other.SampleDecimal &&
                   SampleBool == other.SampleBool && SampleDateTime.Equals(other.SampleDateTime) &&
                   SampleTimeSpan.Equals(other.SampleTimeSpan) && SampleEnum == other.SampleEnum &&
                   SampleFlagEnum == other.SampleFlagEnum && SampleNullableUInt32 == other.SampleNullableUInt32;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SettingsObjectFields)obj);
        }

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
