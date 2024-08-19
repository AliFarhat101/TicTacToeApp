using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace TicTacToeServer
{
    class Program
    {
        private static TcpListener server;
        private static List<TcpClient> clients = new List<TcpClient>();
        private static string[] board = new string[9];
        private static bool gameOver = false;

        static void Main(string[] args)
        {
            const int SERVER_PORT = 50744;
            server = new TcpListener(IPAddress.Any, SERVER_PORT);
            server.Start();
            Console.WriteLine("Tic Tac Toe server started.");
            Console.WriteLine("Waiting for players...");

            Thread clientAcceptThread = new Thread(AcceptClients);
            clientAcceptThread.Start();

            InitializeBoard();
            while (!gameOver)
            {
                Thread.Sleep(1000);
            }
        }

        private static void AcceptClients()
        {
            int k = 1;
            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                lock (clients)
                {
                    clients.Add(client);
                    Console.WriteLine("Client {0} connected.", k);
                    k++;
                }
                Thread clientThread = new Thread(() => HandleClient(client));
                clientThread.Start();
                if (k == 3) { break; }
            }
        }

        private static void HandleClient(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                ProcessClientMessage(message);
            }

            lock (clients)
            {
                clients.Remove(client);
            }
            client.Close();
        }

        private static void ProcessClientMessage(string message)
        {
            string[] parts = message.Split(',');
            if (parts[0] == "MOVE")
            {
                int index = int.Parse(parts[1]);
                string playerSymbol = parts[2];

                if (board[index] == "")
                {
                    board[index] = playerSymbol;
                    BroadcastMessage($"MOVE,{index},{playerSymbol}");

                    if (CheckWinner(playerSymbol))
                    {
                        BroadcastMessage($"WINNER,{playerSymbol}");
                        gameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        BroadcastMessage("DRAW");
                        gameOver = true;
                    }
                }
            }
        }

        private static void BroadcastMessage(string message)
        {
            byte[] data = Encoding.ASCII.GetBytes(message);
            lock (clients)
            {
                foreach (var client in clients)
                {
                    NetworkStream stream = client.GetStream();
                    stream.Write(data, 0, data.Length);
                }
            }
        }

        private static void InitializeBoard()
        {
            for (int i = 0; i < board.Length; i++)
            {
                board[i] = "";
            }
        }

        private static bool CheckWinner(string playerSymbol)
        {
            // Check rows, columns, and diagonals
            return (board[0] == playerSymbol && board[1] == playerSymbol && board[2] == playerSymbol) ||
                   (board[3] == playerSymbol && board[4] == playerSymbol && board[5] == playerSymbol) ||
                   (board[6] == playerSymbol && board[7] == playerSymbol && board[8] == playerSymbol) ||
                   (board[0] == playerSymbol && board[3] == playerSymbol && board[6] == playerSymbol) ||
                   (board[1] == playerSymbol && board[4] == playerSymbol && board[7] == playerSymbol) ||
                   (board[2] == playerSymbol && board[5] == playerSymbol && board[8] == playerSymbol) ||
                   (board[0] == playerSymbol && board[4] == playerSymbol && board[8] == playerSymbol) ||
                   (board[2] == playerSymbol && board[4] == playerSymbol && board[6] == playerSymbol);
        }

        private static bool IsBoardFull()
        {
            foreach (var cell in board)
            {
                if (cell == "")
                {
                    return false;
                }
            }
            return true;
        }
    }
}
