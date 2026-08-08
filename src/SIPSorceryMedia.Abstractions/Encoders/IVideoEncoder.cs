//-----------------------------------------------------------------------------
// Filename: IVideoEncoder.cs
//
// Description: Common interface for a video codec.
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

public interface IVideoEncoder : IDisposable
{
    /// <summary>
    /// Needs to be set with the list of video formats that the encoder supports.
    /// </summary>
    List<VideoFormat> SupportedFormats { get; }

    [Obsolete("Use ReadOnlySpan<byte> overload in order to reduce memory allocations.")]
    byte[] EncodeVideo(int width, int height, byte[] sample, VideoPixelFormatsEnum pixelFormat, VideoCodecsEnum codec);
    bool EncodeVideo(IBufferWriter<byte> output, int width, int height, ReadOnlySpan<byte> sample, VideoPixelFormatsEnum pixelFormat, VideoCodecsEnum codec);

    [Obsolete("Use ReadOnlySpan<byte> overload in order to reduce memory allocations.")]
    byte[] EncodeVideoFaster(RawImage rawImage, VideoCodecsEnum codec); // Avoid to use byte[] to improve performance
    bool EncodeVideoFaster(IBufferWriter<byte> output, RawImage rawImage, VideoCodecsEnum codec); // Avoid to use byte[] to improve performance

    void ForceKeyFrame();

    [Obsolete("Use ReadOnlySpan<byte> overload in order to reduce memory allocations.")]
    IEnumerable<VideoSample> DecodeVideo(byte[] encodedSample, VideoPixelFormatsEnum pixelFormat, VideoCodecsEnum codec);
    IEnumerable<VideoSample> DecodeVideo(ReadOnlySpan<byte> encodedSample, VideoPixelFormatsEnum pixelFormat, VideoCodecsEnum codec);

    [Obsolete("Use ReadOnlySpan<byte> overload in order to reduce memory allocations.")]
    IEnumerable<RawImage> DecodeVideoFaster(byte[] encodedSample, VideoPixelFormatsEnum pixelFormat, VideoCodecsEnum codec); // Avoid to use byte[] to improve performance
    IEnumerable<RawImage> DecodeVideoFaster(ReadOnlySpan<byte> encodedSample, VideoPixelFormatsEnum pixelFormat, VideoCodecsEnum codec); // Avoid to use byte[] to improve performance
}
