using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace NoNameLib.Net.WebSocket
{
    public class WebSocketClient
    {
        private readonly Socket clientSocket;

        public event EventHandler<Packet.Packet> OnPacketReceived;

        internal WebSocketClient(Socket socket)
        {
            this.clientSocket = socket;

            Task.Factory.StartNew(HandleConnectionStream);
        }

        void HandleConnectionStream()
        {
            while (clientSocket.Connected)
            {
                var buffer = new byte[Packet.Packet.PACKET_MAXSIZE];
                var bufferSize = clientSocket.Receive(buffer);
                Array.Resize(ref buffer, bufferSize);
                
                var packet = new Packet.Packet(buffer);
                packet.GetHeader();

                if (OnPacketReceived != null)
                    OnPacketReceived(this, packet);
            }
        }

        #region Public Methods

        public void Send(Packet.Packet packet)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                clientSocket.Send(packet.GetBuffer());
            }
        }

        public void Disconnect()
        {
            clientSocket.Disconnect(false);
        }

        #endregion
    }
}
