using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace SIPSorcery.Sys;

internal static class MemoryOperations
{
    public static string ToLowerString(this ReadOnlySpan<char> span)
    {
        var stringBuilder = new StringBuilder(span.Length);

        foreach (var c in span)
        {
            stringBuilder.Append(char.ToLower(c));
        }

        return stringBuilder.ToString();
    }

#if NETSTANDARD2_0 || NETFRAMEWORK
    unsafe
#endif
    public static byte[] ToLittleEndianBytes(this ReadOnlySpan<short> shorts)
    {
        var bytes = new byte[shorts.Length * 2];

#if NETSTANDARD2_0 || NETFRAMEWORK
        fixed (byte* destPtr = bytes)
        {
            var current = destPtr;

            for (var i = 0; i < shorts.Length; i++)
            {
                var value = shorts[i];

                // Write in little-endian order
                current[0] = (byte)(value & 0xFF);          // Low byte
                current[1] = (byte)((value >> 8) & 0xFF);   // High byte

                current += 2;
            }
        }
#else
        ref var source = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(shorts));
        ref var destination = ref MemoryMarshal.GetReference(bytes.AsSpan());

        for (var i = shorts.Length; i > 0; i--)
        {
            var destSpan = MemoryMarshal.CreateSpan(ref destination, 2);
            BinaryPrimitives.WriteInt16LittleEndian(destSpan, source);

            source = ref Unsafe.Add(ref source, 1);
            destination = ref Unsafe.Add(ref destination, 2);
        }
#endif

        return bytes;
    }

#if NETSTANDARD2_0 || NETFRAMEWORK
    unsafe
#endif
    public static byte[] ToBigEndianBytes(this ReadOnlySpan<short> shorts)
    {
        var bytes = new byte[shorts.Length * 2];

#if NETSTANDARD2_0 || NETFRAMEWORK
        fixed (byte* destPtr = bytes)
        {
            var current = destPtr;

            for (var i = 0; i < shorts.Length; i++)
            {
                var value = shorts[i];


                // Write in big-endian order
                current[0] = (byte)((value >> 8) & 0xFF);   // High byte
                current[1] = (byte)(value & 0xFF);          // Low byte


                current += 2;
            }
        }
#else
        ref var source = ref MemoryMarshal.GetReference(MemoryMarshal.AsBytes(shorts));
        ref var destination = ref MemoryMarshal.GetReference(bytes.AsSpan());

        for (var i = shorts.Length; i > 0; i--)
        {
            var destSpan = MemoryMarshal.CreateSpan(ref destination, 2);
            BinaryPrimitives.WriteInt16BigEndian(destSpan, source);

            source = ref Unsafe.Add(ref source, 1);
            destination = ref Unsafe.Add(ref destination, 2);
        }
#endif

        return bytes;
    }
}
