using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

public partial class Form2 : Form
{
    static char[] board = new char[9] { '1', '2', '3', '4', '5', '6', '7', '8', '9' };
    static char currentPlayer = 'X';
    static UdpClient udpServer;
    static IPEndPoint player1EndPoint;
    static IPEndPoint player2EndPoint;

    static void Main(string[] args)
    {
        udpServer = new UdpClient(5000);

        player1EndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] player1Data = udpServer.Receive(ref player1EndPoint);
        SendMessage(player1EndPoint, "P1");

        player2EndPoint = new IPEndPoint(IPAddress.Any, 0);
        byte[] player2Data = udpServer.Receive(ref player2EndPoint);
        SendMessage(player2EndPoint, "P2");

        bool gameRunning = true;
        while (gameRunning)
        {
            SendBoard(player1EndPoint);
            SendBoard(player2EndPoint);

            if (CheckForWinner())
            {
                SendMessage(player1EndPoint, "Победитель: " + currentPlayer);
                SendMessage(player2EndPoint, "Победитель: " + currentPlayer);
                gameRunning = false;
            }
            else if (IsBoardFull())
            {
                SendMessage(player1EndPoint, "Ничья");
                SendMessage(player2EndPoint, "Ничья");
                gameRunning = false;
            }
            else
            {
                IPEndPoint currentEndPoint = currentPlayer == 'X' ? player1EndPoint : player2EndPoint;
                SendMessage(currentEndPoint, "Ваш ход");

                int move = GetMove(currentEndPoint);
                if (MakeMove(move))
                {
                    if (CheckForWinner())
                    {
                        SendBoard(player1EndPoint);
                        SendBoard(player2EndPoint);
                        SendMessage(player1EndPoint, "Победитель: " + currentPlayer);
                        SendMessage(player2EndPoint, "Победитель: " + currentPlayer);
                        gameRunning = false;
                    }
                    else if (IsBoardFull())
                    {
                        SendMessage(player1EndPoint, "Ничья");
                        SendMessage(player2EndPoint, "Ничья");
                        gameRunning = false;
                    }

                    SwitchPlayer();
                }
            }
        }

        udpServer.Close();
    }

    static void SendMessage(IPEndPoint endPoint, string message)
    {
        byte[] data = Encoding.ASCII.GetBytes(message);
        udpServer.Send(data, data.Length, endPoint);
    }

    static void SendBoard(IPEndPoint endPoint)
    {
        string boardState = GetBoardState();
        SendMessage(endPoint, boardState);
    }

    static string GetBoardState()
    {
        return $" {board[0]} | {board[1]} | {board[2]} \n" +
               "---+---+---\n" +
               $" {board[3]} | {board[4]} | {board[5]} \n" +
               "---+---+---\n" +
               $" {board[6]} | {board[7]} | {board[8]} \n";
    }

    static int GetMove(IPEndPoint endPoint)
    {
        byte[] buffer = udpServer.Receive(ref endPoint);
        string move = Encoding.ASCII.GetString(buffer).Trim();
        return int.Parse(move) - 1;
    }

    static bool MakeMove(int position)
    {
        if (board[position] != 'X' && board[position] != 'O')
        {
            board[position] = currentPlayer;
            return true;
        }
        return false;
    }

    static void SwitchPlayer()
    {
        currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
    }

    static bool CheckForWinner()
    {
        int[,] winPositions = {
            {0, 1, 2},
            {3, 4, 5},
            {6, 7, 8},
            {0, 3, 6},
            {1, 4, 7},
            {2, 5, 8},
            {0, 4, 8},
            {2, 4, 6}
        };

        for (int i = 0; i < winPositions.GetLength(0); i++)
        {
            if (board[winPositions[i, 0]] == currentPlayer &&
                board[winPositions[i, 1]] == currentPlayer &&
                board[winPositions[i, 2]] == currentPlayer)
            {
                return true;
            }
        }

        return false;
    }

    static bool IsBoardFull()
    {
        foreach (char c in board)
        {
            if (c != 'X' && c != 'O')
                return false;
        }
        return true;
    }
}