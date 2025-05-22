using System;
using System.Buffers;
using Org.BouncyCastle.Tls;

namespace SIPSorcery.Sys
{
    internal static class DatagramTransportExtensions
    {
#if NETFRAMEWORK || !NET6_0_OR_GREATER || NETSTANDARD2_0 || NETSTANDARD2_1
        public static void Send(this DatagramSender datagramSender, ReadOnlySpan<byte> buffer)
        {
            var pooled = ArrayPool<byte>.Shared.Rent(buffer.Length);

            try
            {
                buffer.CopyTo(pooled);
                datagramSender.Send(pooled, 0, buffer.Length);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(pooled);
            }
        }
#endif
    }
}
