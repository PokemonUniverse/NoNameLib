namespace NoNameLib.Net.Packet
{
    interface IPacket
    {
        void Reset();
        bool CanWrite(int size);
        bool CanRead(int size);

        int GetHeader();
        void Prepare();

        byte ReadByte();
        short ReadShort();
        int ReadInt();
        long ReadLong();
        string ReadString();

        void WriteByte(byte value);
        void WriteShort(short value);
        void WriteInt(int value);
        void WriteLong(long value);
        void WriteString(string value);
        void WriteBuffer(byte[] value);

        byte[] GetBuffer();
    }
}
