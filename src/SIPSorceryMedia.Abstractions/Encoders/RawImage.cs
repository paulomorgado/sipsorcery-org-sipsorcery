//-----------------------------------------------------------------------------
// Filename: RawImage.cs
//
// Description: A raw image for use with a video codec.
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
using System.Runtime.InteropServices;
using CommunityToolkit.HighPerformance.Buffers;

namespace SIPSorceryMedia.Abstractions;

public class RawImage
{
    /// <summary>
    /// The width, in pixels, of the image
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height, in pixels, of the image
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Integer that specifies the byte offset between the beginning of one scan line and the next.
    /// </summary>
    public int Stride { get; set; }

    /// <summary>
    /// Pointer to an array of bytes that contains the pixel data.
    /// </summary>
    public IntPtr Sample { get; set; }

    /// <summary>
    /// The pixel format of the image
    /// </summary>
    public VideoPixelFormatsEnum PixelFormat { get; set; }

    [Obsolete("Use the overload that takes an IBufferWriter in order to reduce memory allocations.")]
    public byte[] GetBuffer()
    {
        using var buffer = new ArrayPoolBufferWriter<byte>();
        WriteTo(buffer);
        return buffer.WrittenSpan.ToArray();
    }

    /// <summary>
    /// Get bytes array of the image.
    /// For performance reasons it's better to use directly Sample
    /// </summary>
    /// <returns>The number of bytes written to the output buffer.</returns>
    public int WriteTo(IBufferWriter<byte> output)
    {
        if ((Height > 0) && (Stride > 0))
        {
            var bufferSize = Height * Stride;

            // Create a span directly over the unmanaged Sample pointer and copy to output.
            unsafe
            {
                new Span<byte>(Sample.ToPointer(), bufferSize).CopyTo(output.GetSpan(bufferSize));
                output.Advance(bufferSize);
            }

            return bufferSize;
        }

        return 0;
    }
}
