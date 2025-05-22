using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SIPSorcery.Sys
{
    internal static class SocketExtensions
    {
#if !NETSTANDARD1_6_OR_GREATER || NETSTANDARD2_0_OR_GREATER
        public static int SendTo(this Socket socket, ReadOnlySpan<byte> buffer, SocketFlags socketFlags, EndPoint remoteEP)
        {
            return socket.SendTo(buffer, socketFlags, remoteEP);
        }
#endif
    }
}
