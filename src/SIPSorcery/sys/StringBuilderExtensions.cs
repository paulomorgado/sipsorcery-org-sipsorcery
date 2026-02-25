using System;
using System.Collections.Generic;
using System.Text;

namespace SIPSorcery.Sys;

internal static class StringBuilderExtensions
{
    private static readonly char[] upperHexmap = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
    private static readonly char[] lowerHexmap = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

    extension(StringBuilder builder)
    {
        public StringBuilder AppendFormatted<T>(T value, string format, IFormatProvider? provider = null)
        {
            if (value is null)
            {
                return builder;
            }

            if (value is IFormattable)
            {
                return builder.Append(((IFormattable)value).ToString(format: null, provider)); // constrained call avoiding boxing for value types
            }

            return builder.Append(value.ToString());
        }


        public StringBuilder Append(byte[]? bytes, char? separator = null)
        {
            if (bytes is { Length: > 0 })
            {
                builder.Append(bytes.AsSpan(), separator);
            }

            return builder;
        }

        public StringBuilder Append(ReadOnlySpan<byte> bytes, char? separator = null, bool lowercase = false)
        {
            var hexmap = lowercase ? lowerHexmap : upperHexmap;

            if (bytes.IsEmpty)
            {
                return builder;
            }

            if (separator is { } s)
            {
                for (var i = 0; i < bytes.Length;)
                {
                    var b = bytes[i];
                    builder.Append(hexmap[(int)b >> 4]);
                    builder.Append(hexmap[(int)b & 0b1111]);
                    if (++i < bytes.Length)
                    {
                        builder.Append(s);
                    }
                }
            }
            else
            {
                for (var i = 0; i < bytes.Length; i++)
                {
                    var b = bytes[i];
                    builder.Append(hexmap[(int)b >> 4]);
                    builder.Append(hexmap[(int)b & 0b1111]);
                }
            }

            return builder;
        }
    }
}
