using System;
using System.Windows.Forms;

namespace TicTacToeClient
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            bool runagain = true;
            while (runagain)
            {
                Application.Run(new Form1());
                DialogResult result = MessageBox.Show("Do you want to play a new game?", "Play again", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                { runagain = true; }
                else
                { runagain = false; }
            }
        }
    }
}
