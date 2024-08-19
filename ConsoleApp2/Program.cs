using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        int port = 8888;
        TcpListener listener = new TcpListener(ipAddress, port);

        try
        {
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                Console.WriteLine("Test");

                byte[] buffer = new byte[256];
                stream.Read(buffer,0,buffer.Length);
                String request = Encoding.ASCII.GetString(buffer);

                if (request.Equals("date",StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("date");

                    string date = DateTime.Now.ToShortDateString();                                                     
                    byte[] data = Encoding.ASCII.GetBytes(date);

                    stream.Write(data, 0, data.Length);
                }
                else if (request.Equals("time", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("time");

                    string time = DateTime.Now.ToShortTimeString();
                    byte[] data = Encoding.ASCII.GetBytes(time);

                    stream.Write(data, 0, data.Length);
                }
                else
                {
                    Console.WriteLine("test2");
                }

            }
        }
        catch(Exception ex)
        {
                  Console.WriteLine(ex.Message);                            
        }
        finally
        {
            listener.Stop();
        }

    }
}
