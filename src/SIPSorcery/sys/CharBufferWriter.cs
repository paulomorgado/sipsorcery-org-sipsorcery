using System.Runtime.CompilerServices;

namespace System.Buffers;

internal static partial class CharBufferWriter
{
    extension(IBufferWriter<char> writer)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(bool value) => writer.Append(value ? "true" : "false");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(char value)
        {
            writer.GetSpan(1)[0] = value;
            writer.Advance(1);
            return writer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(string? value)
        {
            if (value is not null)
            {
                writer.Append(value.AsSpan());
            }

            return writer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> AppendLine() => writer.Append(Environment.NewLine);

        public IBufferWriter<char> Append(char value, int count)
        {
            var destination = writer.GetSpan(count).Slice(0, count);
            destination.Fill(value);
            writer.Advance(count);
            return writer;
        }

        public IBufferWriter<char> Append(scoped ReadOnlySpan<char> value)
        {
            value.CopyTo(writer.GetSpan(value.Length));
            writer.Advance(value.Length);
            return writer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(int value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(uint value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(ushort value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(ushort? value, string? format = null, IFormatProvider? provider = null)
        {
            if (value is { } actualValue)
            {
#if NET6_0_OR_GREATER
                writer.AppendSpanFormattable(actualValue, format, provider);
#else
                writer.Append(actualValue.ToString(format, provider));
#endif
            }

            return writer;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(long value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(ulong value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(float value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(double value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(decimal value, string? format = null, IFormatProvider? provider = null)
#if NET6_0_OR_GREATER
            => writer.AppendSpanFormattable(value, format, provider);
#else
            => writer.Append(value.ToString(format, provider));
#endif
    }
}
