using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient("127.0.0.1",8888);
            NetworkStream stream = client.GetStream();

            byte[] request = Encoding.ASCII.GetBytes("USD/EURO");
            stream.Write(request, 0, request.Length);

            byte[] data = new byte[256];
            StringBuilder response = new StringBuilder();
            int bytes;

            bytes = stream.Read(data,0,data.Length);
            response.Append(Encoding.ASCII.GetString(data, 0 ,bytes));

            Console.WriteLine(response);

            client.Close();
            Console.ReadLine();
        }
    }
}
