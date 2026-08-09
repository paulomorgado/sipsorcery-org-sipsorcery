//-----------------------------------------------------------------------------
// Filename: IAudioEncoder.cs
//
// Description: Common interface for an audio codec.
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
using System.Collections.Generic;

namespace SIPSorceryMedia.Abstractions;

public interface IAudioEncoder
{
    /// <summary>
    /// Needs to be set with the list of audio formats that the encoder supports.
    /// </summary>
    List<AudioFormat> SupportedFormats { get; }

    [Obsolete("Use the overload that takes IBufferWriter and ReadOnlySpan in order to reduce memory allocations.")]
    byte[] EncodeAudio(short[] pcm, AudioFormat format);

    /// <summary>
    /// Encodes 16bit signed PCM samples.
    /// </summary>
    /// <param name="output">The output buffer to write the encoded sample to.</param>
    /// <param name="pcm">An array of 16 bit signed audio samples.</param>
    /// <param name="format">The audio format to encode the PCM sample to.</param>
    void EncodeAudio(IBufferWriter<byte> output, ReadOnlySpan<short> pcm, AudioFormat format);

    [Obsolete("Use the overload that takes IBufferWriter and ReadOnlySpan in order to reduce memory allocations.")]
    short[] DecodeAudio(byte[] encodedSample, AudioFormat format);

    /// <summary>
    /// Decodes to 16bit signed PCM samples.
    /// </summary>
    /// <param name="output">The output buffer to write the decoded PCM samples to.</param>
    /// <param name="encodedSample">The byte array containing the encoded sample.</param>
    /// <param name="format">The audio format of the encoded sample.</param>
    void DecodeAudio(IBufferWriter<short> output, ReadOnlySpan<byte> encodedSample, AudioFormat format);
}
