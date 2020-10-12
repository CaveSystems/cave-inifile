using System;

namespace Cave
{
    internal static class Ini
    {
        internal static void CheckName(string value, string paramName)
        {
            if (value == null)
            {
                throw new ArgumentNullException(paramName);
            }
            for (var i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case '#':
                    case '[':
                    case ']':
                        throw new ArgumentException($"Invalid name for {paramName} {value}!", paramName);
                    default:
                        if (value[i] < 32)
                        {
                            throw new ArgumentException($"Invalid name for {paramName} {value}!", paramName);
                        }
                        break;
                }
            }
        }

        internal static string Escape(string value, IniProperties properties)
        {
            var box = value.IndexOfAny(new[] { properties.BoxCharacter, '#', ' ' }) > -1;
            if (!properties.DisableEscaping)
            {
                value = value.EscapeUtf8();
            }

            box |= value.IndexOf('\\') > -1 || value.Trim() != value;
            if (box)
            {
                value = value.Box(properties.BoxCharacter);
            }
            return value;
        }

        internal static string Unescape(string value, IniProperties properties)
        {
            if (value.IsBoxed(properties.BoxCharacter, properties.BoxCharacter))
            {
                value = value.Unbox(properties.BoxCharacter);
            }

            if (!properties.DisableEscaping)
            {
                try
                {
                    value = value.Unescape();
                }
                catch
                {
                    // unescape failed, this may be a windows path
                }
            }

            return value;
        }
    }
}