using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;
using SIPSorcery.SIP;

namespace SIPSorcery.core.SIP
{
    internal static partial class SipLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "SIPTransportQueueFull",
            Level = LogLevel.Warning,
            Message = "SIPTransport queue full new message from {RemoteEndPoint} being discarded."
            )]
        public static partial void LogSIPTransportQueueFull(this ILogger logger, SIPEndPoint remoteEndPoint);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPTransportShutdownError",
            Level = LogLevel.Error,
            Message = "Exception SIPTransport Shutdown. {ErrorMessage}"
            )]
        private static partial void LogSIPTransportShutdownErrorImpl(this ILogger logger, Exception excp,string errorMessage);

        public static void LogSIPTransportShutdownError(this ILogger logger, Exception excp)
        {
            LogSIPTransportShutdownErrorImpl(logger, excp, excp.Message);
        }

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPTransportReceiveMessage",
            Level = LogLevel.Error,
            Message = "Exception SIPTransport ReceiveMessage. {ErrorMessage}"
            )]
        private static partial void LogSIPTransportReceiveMessageImpl(this ILogger logger, Exception excp,string errorMessage);

        public static void LogSIPTransportReceiveMessage(this ILogger logger, Exception excp)
        {
            LogSIPTransportReceiveMessageImpl(logger, excp, excp.Message);
        }

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPTransportProcessReceiveQueue",
            Level = LogLevel.Error,
            Message = "Exception SIPTransport ProcessReceiveQueue. {ErrorMessage}"
            )]
        private static partial void LogSIPTransportProcessReceiveQueueImpl(this ILogger logger, Exception excp,string errorMessage);

        public static void LogSIPTransportProcessReceiveQueue(this ILogger logger, Exception excp)
        {
            LogSIPTransportProcessReceiveQueueImpl(logger, excp, excp.Message);
        }

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPMessageReceived",
            Level = LogLevel.Error,
            Message = "Exception SIPMessageReceived. {ErrorMessage}"
            )]
        private static partial void LogSIPMessageReceivedImpl(this ILogger logger, Exception excp, string errorMessage);

        public static void LogSIPMessageReceived(this ILogger logger, Exception excp)
        {
            LogSIPMessageReceivedImpl(logger, excp, excp.Message);
        }

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPRequestIn",
            Level = LogLevel.Debug,
            Message = "SIP request received: {LocalEndPoint}<-{RemoteEndPoint} {StatusLine}"
            )]
        public static partial void LogSIPRequestIn(this ILogger logger, SIPEndPoint localEndPoint, SIPEndPoint remoteEndPoint, string statusLine);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPRequestInRequest",
            Level = LogLevel.Trace,
            Message = "Request: {Request}"
            )]
        public static partial void LogSIPRequestInRequest(this ILogger logger, SIPRequest request);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPRequestOut",
            Level = LogLevel.Debug,
            Message = "SIP request sent: {LocalEndPoint}->{RemoteEndPoint} {StatusLine}"
            )]
        public static partial void LogSIPRequestOut(this ILogger logger, SIPEndPoint localEndPoint, SIPEndPoint remoteEndPoint, string statusLine);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPRequestOutRequest",
            Level = LogLevel.Trace,
            Message = "Request sent: {Request}"
            )]
        public static partial void LogSIPRequestOutRequest(this ILogger logger, SIPRequest request);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPResponseIn",
            Level = LogLevel.Debug,
            Message = "SIP response received: {LocalEndPoint}<-{RemoteEndPoint} {ShortDescription}"
            )]
        public static partial void LogSIPResponseIn(this ILogger logger, SIPEndPoint localEndPoint, SIPEndPoint remoteEndPoint, string shortDescription);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPResponseInRequest",
            Level = LogLevel.Trace,
            Message = "Response received: {Response}"
            )]
        public static partial void LogSIPResponseInRequest(this ILogger logger, SIPResponse response);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPResponseOut",
            Level = LogLevel.Debug,
            Message = "SIP response sent: {LocalEndPoint}->{RemoteEndPoint} {ShortDescription}"
            )]
        public static partial void LogSIPResponseOut(this ILogger logger, SIPEndPoint localEndPoint, SIPEndPoint remoteEndPoint, string shortDescription);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPResponseOutRequest",
            Level = LogLevel.Trace,
            Message = "Response sent: {Response}"
            )]
        public static partial void LogSIPResponseOutRequest(this ILogger logger, SIPResponse response);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPRequestRetransmit",
            Level = LogLevel.Debug,
            Message = "SIP request retransmit {Count} for request {StatusLine}, initial transmit {InitialTransmit}s ago."
            )]
        public static partial void LogSIPRequestRetransmit(this ILogger logger, int count, string statusLine, double initialTransmit);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPRequestRetransmitRequest",
            Level = LogLevel.Trace,
            Message = "Request retransmitted: {Response}"
            )]
        public static partial void LogSIPRequestRetransmitRequest(this ILogger logger, SIPRequest response);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPResponseRetransmit",
            Level = LogLevel.Debug,
            Message = "SIP response retransmit {Count} for response {ShortDescription}, initial transmit {InitialTransmit}s ago."
            )]
        public static partial void LogSIPResponseRetransmit(this ILogger logger, int count, string shortDescription, double initialTransmit);

        [LoggerMessage(
            EventId = 0,
            EventName = "SIPResponseRetransmitResponse",
            Level = LogLevel.Trace,
            Message = "Response retransmitted: {Response}"
            )]
        public static partial void LogSIPResponseRetransmitRequest(this ILogger logger, SIPResponse response);
    }
}
