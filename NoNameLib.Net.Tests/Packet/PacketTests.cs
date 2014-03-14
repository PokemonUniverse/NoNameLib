using Microsoft.VisualStudio.TestTools.UnitTesting;

using NetPacket = NoNameLib.Net.Packet;

namespace NoNameLib.Net.Tests.Packet
{
    [TestClass]
    public class PacketTests
    {
        [TestMethod]
        public void WriteReadPacket()
        {
            var packet = new NetPacket.Packet();
            packet.WriteByte(1);
            packet.WriteInt(int.MaxValue);
            packet.WriteShort(short.MaxValue);         
            packet.WriteLong(long.MaxValue);
            packet.WriteString("Hello World");
            
            packet.Prepare();
            packet.Reset();

            Assert.AreEqual(1, packet.ReadByte(), "Failed to read byte value");
            Assert.AreEqual(int.MaxValue, packet.ReadInt(), "Failed to read int value");
            Assert.AreEqual(short.MaxValue, packet.ReadShort(), "Failed to read short value");
            Assert.AreEqual(long.MaxValue, packet.ReadLong(), "Failed to read long value");
            Assert.AreEqual("Hello World", packet.ReadString(), "Failed to read string value");
        }
    }
}
