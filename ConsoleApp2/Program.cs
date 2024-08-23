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
        TcpListener listener = new TcpListener(IPAddress.Any, 8888);
        double euro = 0.98;
        double usd = 1;

        try
        {
            listener.Start();

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                NetworkStream stream = client.GetStream();

                using (StreamWriter writer = new StreamWriter("log.txt", true)) 
                {
                    writer.WriteLine("Start: " + client.Client.ToString() + " " + DateTime.Now.ToString());
                    writer.Flush();  
                }


                byte[] buffer = new byte[256];
                int bytes = stream.Read(buffer, 0, buffer.Length);
                string request = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

                Console.WriteLine(request + " :value");

                if (request.Equals("EURO/USD", StringComparison.OrdinalIgnoreCase))
                {

                    string euroUsd = usd.ToString();                                                     
                    byte[] data = Encoding.ASCII.GetBytes(euroUsd);

                    stream.Write(data, 0, data.Length);
                }
                else if (request.Equals("USD/EURO", StringComparison.OrdinalIgnoreCase))
                {

                    string UsdEuro = Math.Round((usd / euro),2).ToString();
                    byte[] data = Encoding.ASCII.GetBytes(UsdEuro);

                    stream.Write(data, 0, data.Length);
                }
                using (StreamWriter writer = new StreamWriter("log.txt", true))
                {
                    writer.WriteLine("END: " + client.Client.ToString() + " " + DateTime.Now.ToString());
                    writer.Flush();
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
