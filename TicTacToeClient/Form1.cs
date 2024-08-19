using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace TicTacToeClient
{
    public partial class Form1 : Form
    {
        private const string SERVER_IP = "192.168.1.2"; // Change this to your server's IP address
        private const int SERVER_PORT = 50744; // Change this to your server's port
        private TcpClient client;
        private NetworkStream stream;
        private Thread receiveThread;
        private bool playerTurn = true; // true = X turn; false = O turn

        public Form1()
        {
            InitializeComponent();
            InitializeGame();
            InitializeNetwork();
        }

        private void InitializeNetwork()
        {
            try
            {
                client = new TcpClient();
                client.Connect(SERVER_IP, SERVER_PORT);
                stream = client.GetStream();
                receiveThread = new Thread(new ThreadStart(ReceiveData));
                receiveThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Your Game is ready to launch!");
            }
        }

        private void SendData(string message)
        {
            try
            {
                if (client != null && client.Connected && stream != null)
                {
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error sending data: " + ex.Message);
            }
        }


        private void ReceiveData()
        {
            try
            {
                byte[] data = new byte[256];
                int bytes;
                while ((bytes = stream.Read(data, 0, data.Length)) != 0)
                {
                    string response = Encoding.ASCII.GetString(data, 0, bytes);
                    Invoke((MethodInvoker)delegate
                    {
                        ProcessServerResponse(response);
                    });
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show("Error receiving data: " + ex.Message);
            }
        }

        private void ProcessServerResponse(string response)
        {
            string[] parts = response.Split(',');
            switch (parts[0])
            {
                case "MOVE":
                    int buttonIndex = int.Parse(parts[1]);
                    string playerSymbol = parts[2];
                    Button button = Controls.Find($"btn{buttonIndex + 1}", true)[0] as Button;
                    button.Text = playerSymbol;
                    lblStatus.Text = playerTurn ? "Your turn" : "Opponent's turn";
                    playerTurn = !playerTurn;
                    break;
                case "WINNER":
                    string winner = parts[1];
                    lblStatus.Text = $"{winner} wins!";
                    DisableButtons();
                    break;
                case "DRAW":
                    lblStatus.Text = "It's a draw!";
                    break;
            }
        }

        private void InitializeGame()
        {
            btn1.Click += new EventHandler(ButtonClick);
            btn2.Click += new EventHandler(ButtonClick);
            btn3.Click += new EventHandler(ButtonClick);
            btn4.Click += new EventHandler(ButtonClick);
            btn5.Click += new EventHandler(ButtonClick);
            btn6.Click += new EventHandler(ButtonClick);
            btn7.Click += new EventHandler(ButtonClick);
            btn8.Click += new EventHandler(ButtonClick);
            btn9.Click += new EventHandler(ButtonClick);
        }

        private string GetSymbolBasedOnBlankSpotsCount(string[,] board)
        {
            int blankSpotsCount = 0;

            foreach (string cell in board)
            {
                if (cell == "")
                {
                    blankSpotsCount++;
                }
            }

            return blankSpotsCount % 2 == 0 ? "O" : "X";
        }

        private void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;

            if (button.Text == "")
            {
                int buttonIndex = int.Parse(button.Name.Substring(3)) - 1;
                string[,] board = new string[3, 3]
                 {
                    { btn1.Text, btn2.Text, btn3.Text },
                    { btn4.Text, btn5.Text, btn6.Text },
                    { btn7.Text, btn8.Text, btn9.Text }
                 };
                string playerSymbol = GetSymbolBasedOnBlankSpotsCount(board);
                //string playerSymbol = playerTurn ? "X" : "O";
                button.Text = playerSymbol;
                lblStatus.Text = playerTurn ? "Opponent's turn" : "Your turn";
                playerTurn = !playerTurn;

                SendData($"MOVE,{buttonIndex},{playerSymbol}");
                CheckWinner();
            }
        }

        private void CheckWinner()
        {
            string[,] board = new string[3, 3]
            {
                { btn1.Text, btn2.Text, btn3.Text },
                { btn4.Text, btn5.Text, btn6.Text },
                { btn7.Text, btn8.Text, btn9.Text }
            };

            // Check rows, columns, and diagonals
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == board[i, 1] && board[i, 1] == board[i, 2] && board[i, 0] != "")
                {
                    ShowWinner(board[i, 0]);
                    return;
                }

                if (board[0, i] == board[1, i] && board[1, i] == board[2, i] && board[0, i] != "")
                {
                    ShowWinner(board[0, i]);
                    return;
                }
            }

            if (board[0, 0] == board[1, 1] && board[1, 1] == board[2, 2] && board[0, 0] != "")
            {
                ShowWinner(board[0, 0]);
                return;
            }

            if (board[0, 2] == board[1, 1] && board[1, 1] == board[2, 0] && board[0, 2] != "")
            {
                ShowWinner(board[0, 2]);
                return;
            }

            if (IsBoardFull())
            {
                lblStatus.Text = "It's a draw!";
            }
        }

        private bool IsBoardFull()
        {
            return btn1.Text != "" && btn2.Text != "" && btn3.Text != "" &&
                   btn4.Text != "" && btn5.Text != "" && btn6.Text != "" &&
                   btn7.Text != "" && btn8.Text != "" && btn9.Text != "";
        }

        private void ShowWinner(string winner)
        {
            lblStatus.Text = $"{winner} wins!";
            DisableButtons();
        }

        private void DisableButtons()
        {
            btn1.Enabled = false;
            btn2.Enabled = false;
            btn3.Enabled = false;
            btn4.Enabled = false;
            btn5.Enabled = false;
            btn6.Enabled = false;
            btn7.Enabled = false;
            btn8.Enabled = false;
            btn9.Enabled = false;
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (receiveThread != null && receiveThread.IsAlive)
            {
                receiveThread.Abort();
            }
            if (client != null && client.Connected)
            {
                client.Close();
            }
        }
    }
}
