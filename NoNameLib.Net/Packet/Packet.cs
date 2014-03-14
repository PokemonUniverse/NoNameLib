using System;
using System.Text;
using NoNameLib.Net.Exceptions;

namespace NoNameLib.Net.Packet
{
    public class Packet : IPacket
    {
        public enum PacketException
        {
            UnableToReadBytes,
            UnableToWriteBytes
        }

        public const int PACKET_MAXSIZE = 16384;

        #region Fields

        private readonly byte[] buffer;

        private int readPosition;

        #endregion

        public Packet() 
            : this(new byte[PACKET_MAXSIZE])
        {
        }

        public Packet(byte[] buffer)
        {
           this. buffer = buffer;

            Reset();
        }

        #region Properties

        public int Size { get; private set; }

        #endregion

        #region Public Methods

        public byte[] GetBuffer()
        {
            return new ArraySegment<byte>(buffer, 0, Size).Array;
        }

        public void Reset()
        {
            Size = 0;
            readPosition = 2;
        }

        public int GetHeader()
        {
            Size = (buffer[0] | (buffer[1] << 8));
            return Size;
        }

        /// <summary>
        /// Prepare packet for sending. This will add the packet size as header.
        /// </summary>
        public void Prepare()
        {
            buffer[0] = (byte)(Size >> 8);
            buffer[1] = (byte)Size;
        }

        public bool CanWrite(int length)
        {
            return ((length + readPosition) < (PACKET_MAXSIZE - 16));
        }

        public bool CanRead(int length)
        {
            return ((length + readPosition) < PACKET_MAXSIZE);
        }

        public byte ReadByte()
        {
            if (!CanRead(0))
            {
                throw new NetworkingException(PacketException.UnableToReadBytes, "Unable to read {0} bytes. (readPosition={1}, bufferLength={2})", 1, readPosition, PACKET_MAXSIZE);
            }

            var v = buffer[readPosition];
            readPosition += 1;
            return v;
        }

        public void WriteByte(byte value)
        {
            if (!CanWrite(1))
            {
                throw new NetworkingException(PacketException.UnableToWriteBytes, "Unable to write {0} bytes. (readPosition={1}, bufferLength={2})", 1, readPosition, PACKET_MAXSIZE);
            }

            buffer[readPosition] = value;
            readPosition += 1;
            Size += 1;
        }

        public bool ReadBool()
        {
            byte v = ReadByte();
            return (v == 1);
        }

        public void WriteBool(bool value)
        {
            WriteByte((byte)(value ? 1 : 0));
        }

        public short ReadShort()
        {
            if (!CanRead(1))
            {
                throw new NetworkingException(PacketException.UnableToReadBytes, "Unable to read {0} bytes. (readPosition={1}, bufferLength={2})", 2, readPosition, PACKET_MAXSIZE);
            }

            var v = (short)(buffer[readPosition] | (short)(buffer[readPosition + 1] << 8));
            readPosition += 2;
            return v;
        }

        public void WriteShort(short value)
        {
            if (!CanWrite(2))
            {
                throw new NetworkingException(PacketException.UnableToWriteBytes, "Unable to write {0} bytes. (readPosition={1}, bufferLength={2})", 2, readPosition, PACKET_MAXSIZE);
            }

            buffer[readPosition] = (byte)value;
            readPosition++;
            buffer[readPosition] = (byte)(value >> 8);
            readPosition++;

            Size += 2;
        }

        public int ReadInt()
        {
            if (!CanRead(3))
            {
                throw new NetworkingException(PacketException.UnableToReadBytes, "Unable to read {0} bytes. (readPosition={1}, bufferLength={2})", 4, readPosition, PACKET_MAXSIZE);
            }

            var v = (buffer[readPosition] | ((buffer[readPosition + 1] << 8))
                     | ((buffer[readPosition + 2]) << 16) | ((buffer[readPosition + 3]) << 24));
            readPosition += 4;
            return v;
        }

        public void WriteInt(int value)
        {
            if (!CanWrite(4))
            {
                throw new NetworkingException(PacketException.UnableToWriteBytes, "Unable to write {0} bytes. (readPosition={1}, bufferLength={2})", 4, readPosition, PACKET_MAXSIZE);
            }

            buffer[readPosition] = (byte)value;
            readPosition++;
            buffer[readPosition] = (byte)(value >> 8);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 16);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 24);
            readPosition++;

            Size += 4;
        }

        public long ReadLong()
        {
            if (!CanRead(7))
            {
                throw new NetworkingException(PacketException.UnableToReadBytes, "Unable to read {0} bytes. (readPosition={1}, bufferLength={2})", 8, readPosition, PACKET_MAXSIZE);
            }

            var v = ((buffer[readPosition]) | (((long)buffer[readPosition + 1] << 8))
                     | (((long)buffer[readPosition + 2]) << 16) | (((long)buffer[readPosition + 3]) << 24)
                     | (((long)buffer[readPosition + 4]) << 32) | (((long)buffer[readPosition + 5] << 40))
                     | (((long)buffer[readPosition + 6]) << 48) | (((long)buffer[readPosition + 7]) << 56));
            readPosition += 8;
            return v;
        }

        public void WriteLong(long value)
        {
            if (!CanWrite(8))
            {
                throw new NetworkingException(PacketException.UnableToWriteBytes, "Unable to write {0} bytes. (readPosition={1}, bufferLength={2})", 8, readPosition, PACKET_MAXSIZE);
            }

            buffer[readPosition] = (byte)value;
            readPosition++;
            buffer[readPosition] = (byte)(value >> 8);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 16);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 24);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 32);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 40);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 48);
            readPosition++;
            buffer[readPosition] = (byte)(value >> 56);
            readPosition++;

            Size += 8;
        }

        public string ReadString()
        {
            var stringLength = ReadShort();

            if (!CanRead(stringLength))
            {
                throw new NetworkingException(PacketException.UnableToReadBytes, "Unable to read {0} bytes. (readPosition={1}, bufferLength={2})", stringLength, readPosition, PACKET_MAXSIZE);
            }

            var value = new StringBuilder(stringLength);
            for (int i = 0; i < stringLength; i++)
            {
                value.Append((char)buffer[readPosition++]);
            }

            return value.ToString();
        }

        public void WriteString(string value)
        {
            var stringLength = value.Length;
            if (!CanWrite(stringLength))
            {
                throw new NetworkingException(PacketException.UnableToWriteBytes, "Unable to write {0} bytes. (readPosition={1}, bufferLength={2})", stringLength, readPosition, PACKET_MAXSIZE);
            }

            // First write length of the string
            WriteShort((short)stringLength);

            // Convert string to byte array and add it to buffer
            WriteBuffer(Encoding.UTF8.GetBytes(value));
        }

        public void WriteBuffer(byte[] value)
        {
            if (!CanWrite(value.Length))
            {
                throw new NetworkingException(PacketException.UnableToWriteBytes, "Unable to write {0} bytes. (readPosition={1}, bufferLength={2})", value.Length, readPosition, PACKET_MAXSIZE);
            }

            foreach (var b in value)
            {
                buffer[readPosition++] = b;
            }

            Size += value.Length;
        }

        #endregion
    }
}
