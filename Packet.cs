using System;

namespace Packet_Assembler
{
    public class Packet
    {
        // "Coordinates" for the position of the packet in the stream. 
        public int PacketId;
        public int PacketIndex;
        public int PacketMaxAmount;

        public string Message;
        public string RawString;

        public Packet(int packetId, int packetIndex, int packetMaxIndex, string message, string raw)
        {
            PacketId = packetId;
            PacketIndex = packetIndex;
            PacketMaxAmount = packetMaxIndex;
            Message = message;
            RawString = raw;
        }
    }
}