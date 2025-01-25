using System;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto.Tls;
using SIPSorcery.Net;

namespace SIPSorcery.net.WebRTC
{
    internal static partial class WebRtcLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "DtlsCertificate",
            Level = LogLevel.Trace,
            Message = "-----BEGIN CERTIFICATE-----\n{Certificate}\n-----END CERTIFICATE-----",
            SkipEnabledCheck = true
            )]
        private static partial void LogDtlsCertificateImpl(this ILogger logger, string certificate);

        public static void LogDtlsCertificate(this ILogger logger, Certificate dtlsCertificate)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogDtlsCertificateImpl(DtlsUtils.ExportToDerBase64(dtlsCertificate));
            }
        }

        [LoggerMessage(
            EventId = 0,
            EventName = "RemoteCertificate",
            Level = LogLevel.Trace,
            Message = "Remote peer DTLS certificate, signature algorithm {RemoteCertificateSignatureAlgorithm}.\n-----BEGIN CERTIFICATE-----\n{Certificate}\n-----END CERTIFICATE-----",
            SkipEnabledCheck = true
            )]
        private static partial void LogRemoteCertificateImpl(this ILogger logger, string remoteCertificateSignatureAlgorithm, string certificate);

        public static void LogRemoteCertificate(this ILogger logger, X509CertificateStructure remoteCertificate)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                logger.LogRemoteCertificateImpl(DtlsUtils.GetSignatureAlgorithm(remoteCertificate), Convert.ToBase64String(remoteCertificate.GetDerEncoded()));
            }
        }
    }
}
