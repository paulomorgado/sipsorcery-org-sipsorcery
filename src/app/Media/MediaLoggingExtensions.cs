using System.Net;
using Microsoft.Extensions.Logging;

namespace SIPSorcery.Media
{
    internal static partial class MediaLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "SettingAudioSourceFormat",
            Level = LogLevel.Debug,
            Message = "Setting audio source format to {AudioFormatID}:{AudioFormatCodec}."
            )]
        public static partial void LogSettingAudioSourceFormat(this ILogger logger, int audioFormatID, SIPSorceryMedia.Abstractions.AudioCodecsEnum audioFormatCodec);

        [LoggerMessage(
            EventId = 0,
            EventName = "MusicFileNotSetOrFound",
            Level = LogLevel.Warning,
            Message = "Music file not set or not found, using default music resource."
            )]
        public static partial void LogMusicFileNotSetOrFound(this ILogger logger);

        [LoggerMessage(
            EventId = 0,
            EventName = "SendingAudioSteamLength",
            Level = LogLevel.Debug,
            Message = "Sending audio stream length {AudioStreamLength}."
            )]
        public static partial void LogSendingAudioSteamLength(this ILogger logger, long audioStreamLength);

        [LoggerMessage(
            EventId = 0,
            EventName = "RtpMediaPacketReceived",
            Level = LogLevel.Trace,
            Message = "audio RTP packet received from {RemoteEndPoint} ssrc {SyncSource} seqnum {SequenceNumber} timestamp {Timestamp} payload type {PayloadType}."
            )]
        public static partial void LogRtpMediaPacketReceived(this ILogger logger, IPEndPoint remoteEndPoint, uint syncSource, ushort sequenceNumber, uint timestamp, int payloadType);
    }
}
