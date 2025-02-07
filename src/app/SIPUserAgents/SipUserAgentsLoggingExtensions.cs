using Microsoft.Extensions.Logging;

namespace SIPSorcery.SIP.App
{
    internal static partial class SipUserAgentsLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "ServerCallCancelled",
            Level = LogLevel.Debug,
            Message = "B2BUserAgent server call was cancelled with reason {CancelReason}")]
        public static partial void LogServerCallCancelled(this ILogger logger, string? cancelReason);

        [LoggerMessage(
            EventId = 1,
            EventName = "ServerCallCancelled",
            Level = LogLevel.Debug,
            Message = "SIPB2BUserAgent Cancel.")]
        public static partial void LogCancel(this ILogger logger);

        [LoggerMessage(
            EventId = 2,
            EventName = "ClientCallFailed",
            Level = LogLevel.Debug,
            Message = "B2BUserAgent client call failed {Error}.")]
        public static partial void LogClientCallFailed(this ILogger logger, string error);

        [LoggerMessage(
            EventId = 3,
            EventName = "ClientCallAnswered",
            Level = LogLevel.Debug,
            Message = "B2BUserAgent client call answered {ShortDescription}.")]
        public static partial void LogClientCallAnswered(this ILogger logger, string shortDescription);

        [LoggerMessage(
            EventId = 5,
            EventName = "OutboundProxy", 
            Level = LogLevel.Debug,
            Message = "SIPClientUserAgent Call using alternate outbound proxy of {ServerEndPoint}.")]
        public static partial void LogOutboundProxy(this ILogger logger, string serverEndPoint);

        [LoggerMessage(
            EventId = 6,
            EventName = "RouteSet",
            Level = LogLevel.Debug,
            Message = "Route set for call {RouteSet}.")]
        public static partial void LogRouteSet(this ILogger logger, string routeSet);

        [LoggerMessage(
            EventId = 7,
            EventName = "DNSLookup",
            Level = LogLevel.Debug,
            Message = "SIPClientUserAgent attempting to resolve {Host}.")]
        public static partial void LogDNSLookup(this ILogger logger, string host);

        [LoggerMessage(
            EventId = 8,
            EventName = "DNSFailure",
            Level = LogLevel.Debug,
            Message = "SIPClientUserAgent DNS failure resolving {Host} in {Duration:0.##}ms. Call cannot proceed.")]
        public static partial void LogDNSFailure(this ILogger logger, string host, double duration);

        [LoggerMessage(
            EventId = 9,
            EventName = "DNSSuccess",
            Level = LogLevel.Debug,
            Message = "SIPClientUserAgent resolved {Host} to {Result} in {Duration:0.##}ms.")]
        public static partial void LogDNSSuccess(this ILogger logger, string host, string result, double duration);

        [LoggerMessage(
            EventId = 10,
            EventName = "CommencingCall",
            Level = LogLevel.Debug,
            Message = "UAC commencing call to {CanonicalAddress}.")]
        public static partial void LogCommencingCall(this ILogger logger, string canonicalAddress);

        [LoggerMessage(
            EventId = 11,
            EventName = "InvalidOutboundProxy",
            Level = LogLevel.Debug,
            Message = "Error an outbound proxy value was not recognised in SIPClientUserAgent Call. {RouteSet}.")]
        public static partial void LogInvalidOutboundProxy(this ILogger logger, string routeSet);

        [LoggerMessage(
            EventId = 12,
            EventName = "EmptyCallBody",
            Level = LogLevel.Debug,
            Message = "Body on UAC call was empty.")]
        public static partial void LogEmptyCallBody(this ILogger logger);

        [LoggerMessage(
            EventId = 13,
            EventName = "CallCancelled",
            Level = LogLevel.Debug,
            Message = "Cancelling forwarded call leg {Uri}, server transaction has not been created yet no CANCEL request required.")]
        public static partial void LogCallCancelled(this ILogger logger, string uri);

        [LoggerMessage(
            EventId = 14,
            EventName = "CallCancelRetry",
            Level = LogLevel.Debug,
            Message = "Call {Uri} has already been cancelled once, trying again.")]
        public static partial void LogCallCancelRetry(this ILogger logger, string uri);

        [LoggerMessage(
            EventId = 15,
            EventName = "CallCancelResponse",
            Level = LogLevel.Debug,
            Message = "Call {Uri} has already responded to CANCEL, probably overlap in messages not re-sending.")]
        public static partial void LogCallCancelResponse(this ILogger logger, string uri);

        [LoggerMessage(
            EventId = 16,
            EventName = "CallCancelSending",
            Level = LogLevel.Debug,
            Message = "Cancelling forwarded call leg, sending CANCEL to {URI}.")]
        public static partial void LogCallCancelSending(this ILogger logger, string uri);

        [LoggerMessage(
            EventId = 17,
            EventName = "Response",
            Level = LogLevel.Debug,
            Message = "Response {StatusCode} {ReasonPhrase} for {URI}.")]
        public static partial void LogResponse(this ILogger logger, string statusCode, string reasonPhrase, string uri);

        [LoggerMessage(
            EventId = 18,
            EventName = "CallCancelledAndAnswered",
            Level = LogLevel.Debug,
            Message = "A cancelled call to {Uri} has been answered AND has already been hungup, no further action being taken.")]
        public static partial void LogCallCancelledAndAnswered(this ILogger logger, string uri);

        [LoggerMessage(
            EventId = 19,
            EventName = "CallCancelledHangingUp",
            Level = LogLevel.Debug,
            Message = "A cancelled call to {Uri} has been answered, hanging up.")]
        public static partial void LogCallCancelledHangingUp(this ILogger logger, string uri);

        [LoggerMessage(
            EventId = 20,
            EventName = "NoContactHeader",
            Level = LogLevel.Debug,
            Message = "No contact header provided on response for cancelled call to {Uri} no further action.")]
        public static partial void LogNoContactHeader(this ILogger logger, string uri);

        [LoggerMessage(
            EventId = 21,
            EventName = "NoCredentials",
            Level = LogLevel.Debug,
            Message = "Forward leg failed, authentication was requested but no credentials were available.")]
        public static partial void LogNoCredentials(this ILogger logger);

        [LoggerMessage(
            EventId = 22,
            EventName = "InformationResponse",
            Level = LogLevel.Debug,
            Message = "Information response {StatusCode} {ReasonPhrase} for {URI}.")]
        public static partial void LogInformationResponse(this ILogger logger, string statusCode, string reasonPhrase, string uri);

        [LoggerMessage(
            EventId = 23,
            EventName = "ServerFinalResponseException",
            Level = LogLevel.Debug,
            Message = "Exception ServerFinalResponseReceived. {ErrorMessage}")]
        public static partial void LogServerFinalResponseException(this ILogger logger, string errorMessage);
    }
}
