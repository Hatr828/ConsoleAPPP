using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            UdpClient client = new UdpClient();
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            byte[] initialMessage = Encoding.ASCII.GetBytes("H");
            client.Send(initialMessage, initialMessage.Length, serverEndPoint);

            byte[] buffer = new byte[256];
            while (true)
            {
                IPEndPoint serverResponseEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] receivedData = client.Receive(ref serverResponseEndPoint);
                string message = Encoding.ASCII.GetString(receivedData);
                Console.WriteLine(message);

                if (message.Contains("YOUR"))
                {
                    Console.Write("(1-9): ");
                    string move = Console.ReadLine();
                    byte[] moveData = Encoding.ASCII.GetBytes(move);
                    client.Send(moveData, moveData.Length, serverEndPoint);
                }
                else if (message.Contains("W") || message.Contains("N"))
                {
                    break;
                }
            }

            Console.WriteLine("LLL");
            client.Close();
        }
    }
}
