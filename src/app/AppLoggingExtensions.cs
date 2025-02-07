using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.Extensions.Logging;

namespace SIPSorcery.SIP.App
{
    internal static partial class AppLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "SDPMangledForRequest",
            Level = LogLevel.Debug,
            Message = "SDP mangled for {Status} response from {RemoteSIPEndPoint}, adjusted address {RemoteEndPointAddress}."
        )]
        public static partial void LogSdpMangledRequest(
            this ILogger logger,
            SIPMethodsEnum status,
            SIPEndPoint remoteSipEndPoint,
            string remoteEndPointAddress);

        [LoggerMessage(
            EventId = 0,
            EventName = "SDPMangledForResponse",
            Level = LogLevel.Debug,
            Message = "SDP mangled for {Status} response from {RemoteSIPEndPoint}, adjusted address {RemoteEndPointAddress}."
        )]
        public static partial void LogSdpMangledResponse(
            this ILogger logger,
            SIPResponseStatusCodesEnum status,
            SIPEndPoint remoteSipEndPoint,
            IPAddress remoteEndPointAddress);
    }
}
