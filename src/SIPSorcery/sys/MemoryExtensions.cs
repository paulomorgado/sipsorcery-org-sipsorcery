using System;
using System.Collections.Generic;

namespace SIPSorcery.Sys;

internal static class MemoryExtensions
{
    extension(ReadOnlySpan<char> value)
    {
        public bool IsEmptyOrWhiteSpace() => value.IsEmpty || value.Trim().IsEmpty;
    }
}
