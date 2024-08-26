using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Xml.Linq;

class Program
{
    static List<ClientHandler> clients = new List<ClientHandler>();
    static object locker = new object();

    static void Main(string[] args)
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 8888);

        try
        {
            listener.Start();
            Console.WriteLine("Сервер запущен...");

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                ClientHandler clientHandler = new ClientHandler(client);
                lock (locker)
                {
                    clients.Add(clientHandler);
                }

                Thread clientThread = new Thread(clientHandler.HandleClient);
                clientThread.Start();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            listener.Stop();
        }
    }

    public static void BroadcastMessage(string message, ClientHandler sender)
    {
        byte[] data = Encoding.UTF8.GetBytes(message);
        lock (locker)
        {
            foreach (var client in clients)
            {
                if (client != sender)
                {
                    client.Stream.Write(data, 0, data.Length);
                }
            }
        }
    }

    public static void RemoveClient(ClientHandler clientHandler)
    {
        lock (locker)
        {
            clients.Remove(clientHandler);
        }
    }
}

class ClientHandler
{
    public TcpClient Client { get; private set; }
    public NetworkStream Stream { get; private set; }
    private string userName;

    public ClientHandler(TcpClient client)
    {
        Client = client;
        Stream = client.GetStream();
    }

    public void HandleClient()
    {
        try
        {
            byte[] buffer = new byte[256];
            int bytes = Stream.Read(buffer, 0, buffer.Length);
            userName = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();

            Console.WriteLine($"{userName} вошел в чат.");
            Program.BroadcastMessage($"{userName} присоединился к чату.", this);

            while (true)
            {
                bytes = Stream.Read(buffer, 0, buffer.Length);
                if (bytes == 0) break;

                string message = Encoding.UTF8.GetString(buffer, 0, bytes).Trim();
                Console.WriteLine($"{userName}: {message}");

                Program.BroadcastMessage($"{userName}: {message}", this);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка у пользователя {userName}: {ex.Message}");
        }
        finally
        {
            Console.WriteLine($"{userName} покинул чат.");
            Program.BroadcastMessage($"{userName} вышел из чата.", this);
            Program.RemoveClient(this);
            Stream.Close();
            Client.Close();
        }
    }
}
