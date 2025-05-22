using System;
using System.Buffers;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using Microsoft.Extensions.ObjectPool;

namespace SIPSorcery.Sys
{
    internal static class SocketAsyncEventArgsPool
    {
        private static readonly ObjectPool<SipSorcerySocketAsyncEventArgs> pool = ObjectPool.Create(new SipSorcerySocketAsyncEventArgsPooledObjectPolicy());

        public static SocketAsyncEventArgs Rent(Memory<byte> buffer, IDisposable? memoryOwner, SocketFlags socketFlags, EventHandler<SocketAsyncEventArgs>? handler)
        {
            var args = pool.Get();

#if NETSTANDARD2_0 || NETFRAMEWORK
            byte[]? pooledArray = null;

            try
            {
                if (!buffer.IsEmpty)
                {
                    if (MemoryMarshal.TryGetArray<byte>(buffer, out var segment))
                    {
                        args.SetBuffer(segment.Array, segment.Offset, segment.Count);
                    }
                    else
                    {
                        pooledArray = ArrayPool<byte>.Shared.Rent(buffer.Length);
                        args.SetBuffer(pooledArray, 0, buffer.Length);
                    }
                }
#else
                args.SetBuffer(buffer);
#endif
                args.SetHandler(handler);
                args.UserToken = memoryOwner;
                args.SocketFlags = socketFlags;
                return args;
#if NETSTANDARD2_0 || NETFRAMEWORK
            }
            finally
            {
                if (pooledArray is not null)
                {
                    ArrayPool<byte>.Shared.Return(pooledArray);
                }
            }
#endif
        }

        public static void Return(SocketAsyncEventArgs args) => pool.Return((SipSorcerySocketAsyncEventArgs)args);

        public static void SendToAsync(this Socket socket, Memory<byte> buffer, IDisposable? memoryOwner, SocketFlags socketFlags, IPEndPoint remoteEndPoint, EventHandler<SocketAsyncEventArgs>? handler)
        {
            var args = Rent(buffer, memoryOwner, socketFlags, handler);
            try
            {
                args.RemoteEndPoint = remoteEndPoint;
                socket.SendToAsync(args);
            }
            finally
            {
                Return(args);
            }
        }

        /// <summary>
        /// A policy for pooling <see cref="SipSorcerySocketAsyncEventArgs"/> instances.
        /// </summary>
        private class SipSorcerySocketAsyncEventArgsPooledObjectPolicy : PooledObjectPolicy<SipSorcerySocketAsyncEventArgs>
        {
            /// <inheritdoc />
            public override SipSorcerySocketAsyncEventArgs Create()
            {
                return new SipSorcerySocketAsyncEventArgs();
            }

            /// <inheritdoc />
            public override bool Return(SipSorcerySocketAsyncEventArgs obj)
            {
                obj.SetHandler(null);
#if !NETSTANDARD2_0 && !NETFRAMEWORK
                obj.SetBuffer(default);
#endif
                obj.SetBuffer(null, 0, 0);

                if (obj.UserToken is IDisposable disposable)
                {
                    disposable.Dispose();
                }

                obj.UserToken = null;

                return true;
            }
        }

        private sealed class SipSorcerySocketAsyncEventArgs : SocketAsyncEventArgs
        {
            private EventHandler<SocketAsyncEventArgs>? _handler;

            /// <summary>Creates an empty <see cref="SipSorcerySocketAsyncEventArgs" /> instance.</summary>
            /// <exception cref="NotSupportedException">The platform is not supported.</exception>
            public SipSorcerySocketAsyncEventArgs()
#if NET5_0_OR_GREATER
                : base(false)
#endif
            {
                Completed += HandleCompleted;
            }

            public void SetHandler(EventHandler<SocketAsyncEventArgs>? handler)
            {
                _handler = handler;
            }

            private void HandleCompleted(object source, SocketAsyncEventArgs e)
            {
                _handler?.Invoke(source, e);
            }
        }
    }
}
