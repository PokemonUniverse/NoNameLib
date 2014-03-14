using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace NoNameLib.Net.WebSocket
{
    public class WebSocketServer
    {
        private readonly TcpListener server;

        private volatile bool isStopping;

        #region Events

        public event EventHandler<string> OnLog;

        #endregion

        public WebSocketServer(int port)
        {
            // TODO: Check if port is in range 0 - 65535

            server = new TcpListener(IPAddress.Any, port);
        }

        /// <summary>
        /// Start listening for new connections. This method will block until stopped.
        /// </summary>
        public IEnumerable<WebSocketClient> Start()
        {
            isStopping = false;
            server.Start();

            Log("WebSocketServer started");

            while (!isStopping)
            {
                Socket socket = null;
                try
                {
                    socket = server.AcceptSocket();
                    if (isStopping)
                    {
                        socket.Shutdown(SocketShutdown.Both);
                        socket.Close();
                    
                        yield break;
                    }
                }
                catch (InvalidOperationException ex)
                {
                    Log("Error accepting new connection - IOE: {0}", ex.Message);
                }
                catch (Exception ex)
                {
                    Log("Error accepting new connection: {0}", ex.Message);
                    yield break;
                }

                if (socket != null)
                {
                    var bytes = new byte[socket.Available];
                    socket.Receive(bytes);

                    //translate bytes of request to string
                    String data = Encoding.UTF8.GetString(bytes);

                    if (new Regex("^GET").IsMatch(data))
                    {
                        var response = Encoding.UTF8.GetBytes("HTTP/1.1 101 Switching Protocols" + Environment.NewLine
                                                              + "Connection: Upgrade" + Environment.NewLine
                                                              + "Upgrade: websocket" + Environment.NewLine
                                                              + "Sec-WebSocket-Accept: " +
                                                              Convert.ToBase64String(SHA1.Create().ComputeHash(Encoding.UTF8.GetBytes(new Regex("Sec-WebSocket-Key: (.*)").Match(data).Groups[1].Value.Trim() + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11")))
                                                              + Environment.NewLine
                                                              + Environment.NewLine);

                        socket.Send(response);
                    }

                    yield return new WebSocketClient(socket);
                }
            }
        }

        public void Stop()
        {
            isStopping = true;
            server.Stop();

            Log("WebSocketServer stopped");
        }

        private void Log(string format, params object[] args)
        {
            if (OnLog != null)
                OnLog(this, string.Format(format, args));
        }
    }
}
