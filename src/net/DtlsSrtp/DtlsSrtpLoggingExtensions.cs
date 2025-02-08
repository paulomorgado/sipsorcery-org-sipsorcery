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
                LogServerCipherSuitNames(logger, ConvertCipherSuitesToNames(serverCipherSuites));
                LogClientCipherSuitNames(logger, ConvertCipherSuitesToNames(offeredCipherSuites));

                static string ConvertCipherSuitesToNames(int[] cipherSuites)
                {
                    string[] cipherSuiteNames = new string[cipherSuites.Length];

                    for (int i = 0; i < cipherSuites.Length; i++)
                    {
                        if (DtlsUtils.CipherSuiteNames.TryGetValue(cipherSuites[i], out string value))
                        {
                            cipherSuiteNames[i] = value;
                        }
                        else
                        {
                            cipherSuiteNames[i] = cipherSuites[i].ToString();
                        }
                    }

                    return string.Join("\n ", cipherSuiteNames);
                }
            }
        }
    }
}
