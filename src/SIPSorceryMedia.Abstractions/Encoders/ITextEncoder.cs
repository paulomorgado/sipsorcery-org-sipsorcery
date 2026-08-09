//-----------------------------------------------------------------------------
// Filename: ITextEncoder.cs
//
// Description: Common interface for a text codec.
//
// Author(s):
// Aaron Clauson (aaron@sipsorcery.com)
// 
// History:
// 20 May 2025  Aaron Clauson   Refactored from MediaEndPoints.
//
// License: 
// BSD 3-Clause "New" or "Revised" License and the additional
// BDS BY-NC-SA restriction, see included LICENSE.md file.
//-----------------------------------------------------------------------------

using System;
using System.Buffers;

namespace SIPSorceryMedia.Abstractions;

public interface ITextEncoder
{
    [Obsolete("Use the overload that takes IBufferWriter and ReadOnlySpan in order to reduce memory allocations.")]
    byte[] EncodeText(char[] text, TextFormat format);

    /// <summary>
    /// Encode a text into a byte array.
    /// </summary>
    /// <param name="output">The output buffer to write the encoded text sample to.</param>
    /// <param name="text">A symbol or text to be transmitted</param>
    /// <param name="format">The text format of the sample.</param>
    void EncodeText(IBufferWriter<byte> output, ReadOnlySpan<char> text, TextFormat format);

    [Obsolete("Use the overload that takes IBufferWriter and ReadOnlySpan in order to reduce memory allocations.")]
    char[] DecodeText(byte[] encodedSample, TextFormat format);

    /// <summary>
    /// Decode a byte array into a string type text.
    /// </summary>
    /// <param name="output">The output buffer to write the decoded text sample to.</param>
    /// <param name="encodedSample">A symbol or text that was received</param>
    /// <param name="format">The text format of the sample.</param>
    void DecodeText(IBufferWriter<char> output, ReadOnlySpan<byte> encodedSample, TextFormat format);
}
