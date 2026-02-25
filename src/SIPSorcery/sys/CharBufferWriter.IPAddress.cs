using System.Net;
using System.Runtime.CompilerServices;

namespace System.Buffers;

internal static partial class CharBufferWriter
{
    extension(IBufferWriter<char> writer)
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public IBufferWriter<char> Append(IPAddress value)
        {
#if NET8_0_OR_GREATER
            return writer.AppendSpanFormattable(value, null, null);
#else
            return writer.Append(value.ToString());
#endif
        }
    }
}
