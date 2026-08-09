//-----------------------------------------------------------------------------
// Filename: H264Packetiser.cs
//
// Description: Contains functions to packetise an H264 Network Abstraction
// Layer Units (NAL or NALU) into an RTP payload.
//
// See "RTP Payload Format for H.264 Video" https://tools.ietf.org/html/rfc6184
//
// Packetisation Modes (see https://tools.ietf.org/html/rfc6184#section-6.2):
// 
// - Mode 0: Single NAL Unit Mode. This is the default mode used when no 
//   packetization-mode parameter is present or when it is set to 0. Only 
//   single NAL unit packets may be used in this mode. STAPs, MTAPs and FUs
//   must not be used.
//
// - Mode 1: Non-interleaved mode. This is the mode used when the
//   packetization-mode=1. Only single NAL unit packets, STAP-As and FU-As 
//   may be used in this mode.
//
// - Mode 2: Interleaved mode. This is the mode used when the
//   packetization-mode=2. This mode is not currently supported.
//
// Author(s):
// Aaron Clauson (aaron@sipsorcery.com)
//
// History:
// 27 Dec 2020	Aaron Clauson	Created, Dublin, Ireland.
//
// License: 
// BSD 3-Clause "New" or "Revised" License, see included LICENSE.md file.
//-----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using WebSocketSharp;

namespace SIPSorcery.Net
{
    public class H264Packetiser
    {
        public const int H264_RTP_HEADER_LENGTH = 2;

        public struct H264Nal
        {
            public byte[] NAL { get; }
            public bool IsLast { get; }

            public H264Nal(byte[] nal, bool isLast)
            {
                NAL = nal;
                IsLast = isLast;
            }
        }

        public ref struct H264NalEnumerator
        {
            private ReadOnlySpan<byte> _accessUnit;
            private int _currPosn;
            private int _searchPosn;
            private int _zeroes;
            private (Range Range, bool IsLast) _current;
            private bool _completed;

            public H264NalEnumerator(ReadOnlySpan<byte> accessUnit)
            {
                _accessUnit = accessUnit;
                _currPosn = 0;
                _searchPosn = 0;
                _zeroes = 0;
                _current = default;
                _completed = false;
            }

            public readonly (Range Range, bool IsLast) Current => _current;

            public bool MoveNext()
            {
                if (_completed)
                    return false;

                // Continue searching from where we left off
                for (var i = _searchPosn; i < _accessUnit.Length; i++)
                {
                    if (_accessUnit[i] == 0x00)
                    {
                        _zeroes++;
                    }
                    else if (_accessUnit[i] == 0x01 && _zeroes >= 2)
                    {
                        var nalStart = i + 1;
                        if (nalStart - _currPosn > 4)
                        {
                            var endPosn = nalStart - ((_zeroes == 2) ? 3 : 4);
                            var nalSize = endPosn - _currPosn;

                            _current = (_currPosn..(endPosn), false);
                            _currPosn = nalStart;
                            _searchPosn = i + 1;
                            _zeroes = 0;
                            return true;
                        }

                        _currPosn = nalStart;
                        _zeroes = 0;
                    }
                    else
                    {
                        _zeroes = 0;
                    }
                }

                // Return the last NAL if any data remains
                if (_currPosn < _accessUnit.Length)
                {
                    _current = (_currPosn.., true);
                    _completed = true;
                    return true;
                }

                _completed = true;
                return false;
            }

            public readonly H264NalEnumerator GetEnumerator() => this;
        }

        public static H264NalEnumerator ParseNals(ReadOnlySpan<byte> accessUnit)
            => new H264NalEnumerator(accessUnit);

        [Obsolete("Use the overload that takes ReadOnlySpan in order to reduce memory allocations.")]
        public static IEnumerable<H264Nal> ParseNals(byte[] accessUnit)
        {
            var accessUnitSpan = accessUnit.AsSpan();
            foreach (var (nal, isLast) in ParseNals(accessUnitSpan))
            {
                yield return new H264Nal(accessUnitSpan[nal].ToArray(), isLast);
            }
        }

        /// <summary>
        /// Constructs the RTP header for an H264 NAL. This method does NOT support
        /// aggregation packets where multiple NALs are sent as a single RTP payload.
        /// The supported H264 header type is Single-Time Aggregation Packet type A 
        /// (STAP-A) and Fragmentation Unit A (FU-A). The headers produced correspond
        /// to H264 packetization-mode=1.
        /// </summary>
        /// <remarks>
        /// RTP Payload Format for H.264 Video:
        /// https://tools.ietf.org/html/rfc6184
        /// 
        /// FFmpeg H264 RTP packetisation code:
        /// https://github.com/FFmpeg/FFmpeg/blob/master/libavformat/rtpenc_h264_hevc.c
        /// 
        /// When the payload size is less than or equal to max RTP payload, send as 
        /// Single-Time Aggregation Packet (STAP):
        /// https://tools.ietf.org/html/rfc6184#section-5.7.1
        /// 
        ///      0                   1                   2                   3
        /// 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// |                          RTP Header                           |
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// |STAP-A NAL HDR |         NALU 1 Size           | NALU 1 HDR    |
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// 
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// |F|NRI|  Type   |                                               |
        /// +-+-+-+-+-+-+-+-+
        /// 
        /// Type = 24 for STAP-A (NOTE: this is the type of the H264 RTP header 
        /// and NOT the NAL type).
        /// 
        /// When the payload size is greater than max RTP payload, send as 
        /// Fragmentation Unit A (FU-A):
        /// https://tools.ietf.org/html/rfc6184#section-5.8
        ///      0                   1                   2                   3
        /// 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1 2 3 4 5 6 7 8 9 0 1
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+
        /// | FU indicator  |   FU header   |                               |
        /// +-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+ 
        /// |   Fragmentation Unit (FU) Payload
        /// |
        /// ...
        /// 
        /// 
        /// The FU indicator octet has the following format:
        ///
        /// +---------------+
        /// |0|1|2|3|4|5|6|7|
        /// +-+-+-+-+-+-+-+-+
        /// |F|NRI|  Type   |
        /// +---------------+
        /// 
        /// F and NRI bits come from the NAL being transmitted.
        /// Type = 28 for FU-A (NOTE: this is the type of the H264 RTP header 
        /// and NOT the NAL type).
        /// 
        /// The FU header has the following format:
        ///
        /// +---------------+
        /// |0|1|2|3|4|5|6|7|
        /// +-+-+-+-+-+-+-+-+
        /// |S|E|R|  Type   |
        /// +---------------+
        /// 
        /// S: Set to 1 for the start of the NAL FU (i.e. first packet in frame).
        /// E: Set to 1 for the end of the NAL FU (i.e. the last packet in the frame).
        /// R: Reserved bit must be 0.
        /// Type: The NAL unit payload type, comes from NAL packet (NOTE: this IS the type of the NAL message).
        /// </remarks>
        public static byte[] GetH264RtpHeader(byte nal0, bool isFirstPacket, bool isFinalPacket)
        {
            byte nalType = (byte)(nal0 & 0x1F);
            //byte nalNri = (byte)((nal0 >> 5) & 0x03);

            byte firstHdrByte = (byte)(nal0 & 0xE0); // Has either 24 (STAP-A) or 28 (FU-A) added to it.

            byte fuIndicator = (byte)(firstHdrByte + 28);
            byte fuHeader = nalType;
            if (isFirstPacket)
            {
                fuHeader += 0x80;
            }
            else if (isFinalPacket)
            {
                fuHeader += 0x40;
            }

            return new byte[] { fuIndicator, fuHeader };
        }
    }
}
