using System.Linq;
using Microsoft.Extensions.Logging;
using SIPSorcery.Net;

namespace SIPSorcery.net.DtlsSrtp
{
    internal static partial class DtlsSrtpLoggingExtensions
    {
        [LoggerMessage(
            EventId = 0,
            EventName = "ClientCipherSuitNames",
            Level = LogLevel.Trace,
            Message = "Client offered cipher suites:\n {ClientCipherSuites}",
            SkipEnabledCheck = true
            )]
        private static partial void LogClientCipherSuitNames(this ILogger logger, string clientCipherSuites);

        [LoggerMessage(
            EventId = 0,
            EventName = "ServerCipherSuitNames",
            Level = LogLevel.Trace,
            Message = "Server offered cipher suites:\n {ServerCipherSuites}",
            SkipEnabledCheck = true
            )]
        private static partial void LogServerCipherSuitNames(this ILogger logger, string serverCipherSuites);

        public static void LogCipherSuitNames(this ILogger logger, int[] serverCipherSuites, int[] offeredCipherSuites)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                // Convert server cipher suites to human-readable names
                var serverCipherSuiteNames = serverCipherSuites
                    .Select(cs => DtlsUtils.CipherSuiteNames.ContainsKey(cs) ? DtlsUtils.CipherSuiteNames[cs] : cs.ToString());

                // Convert client-offered cipher suites to human-readable names
                var clientCipherSuiteNames = offeredCipherSuites
                    .Select(cs => DtlsUtils.CipherSuiteNames.ContainsKey(cs) ? DtlsUtils.CipherSuiteNames[cs] : cs.ToString());

                LogServerCipherSuitNames(logger, string.Join("\n ", serverCipherSuiteNames));
                LogClientCipherSuitNames(logger, string.Join("\n ", clientCipherSuiteNames));
            }
        }
    }
}
