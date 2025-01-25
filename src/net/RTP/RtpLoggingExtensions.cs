using Microsoft.Extensions.Logging;

namespace SIPSorcery.net.RTP
{
    internal static partial class RtpLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "SecureContextAlreadyExists",
            Level = LogLevel.Trace,
            Message = "Tried adding new SecureContext for media type {MediaType}, but one already existed"
            )]
        public static partial void LogSecureContextAlreadyExists(this ILogger logger, Net.SDPMediaTypesEnum mediaType);
    }
}
