using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Packet_Assembler
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Packet> packets = new List<Packet>();

            string packetFile = @"D:\Users\hans2734\Documents\VSCode\Daily Programmer\Packet_Assembler\Packets.txt";
            var lines = File.ReadLines(packetFile);
            foreach (var line in lines)
            {
                Packet packet = CreatePacket(line);

                packets.Add(packet);

                bool isComplete = IsComplete(packets);

                if (isComplete)
                {
                    // get the packets to be printed and sort them
                    var packetsToPrint = packets.Where(x => x.PacketId == packet.PacketId).OrderBy(x => x.PacketIndex);

                    foreach (Packet p in packetsToPrint)
                    {
                        // Print the packets
                        Console.WriteLine(p.RawString);

                        // remove the printed packets from the list
                        packets.RemoveAll(x => x.PacketId == p.PacketId);
                    }
                }


            }
            Console.ReadLine();
        }

        private static Packet CreatePacket(string stdin)
        {
            // trims and  cleans the string of unnecessary spaces due to an issue with LINQ and double spaces being seen as digits and parsing a blank string through
            string cleanedString = System.Text.RegularExpressions.Regex.Replace(stdin, @"\s+", " ").Trim();

            var definitions = cleanedString.Split().Where(x => x.All(char.IsDigit))
                .Select(x => int.Parse(x)).ToList();

            int packetId = definitions[0];
            int packetIndex = definitions[1];
            int packetMaxIndex = definitions[2];
            // Oneliners bois
            string message = new String(cleanedString.ToCharArray().Where(c => !char.IsDigit(c)).ToArray()).TrimStart();

            return new Packet(packetId, packetIndex, packetMaxIndex, message, stdin);
        }

        // Checks all packets currently loaded and checks their IDs to the total amount of packets in a sequence
        private static bool IsComplete(List<Packet> packets)
        {
            bool isLastPacketPresent = packets.Any(x => packets.Where(y => y.PacketId == x.PacketId).Count() == x.PacketMaxAmount);

            return isLastPacketPresent;
        }
    }
}
