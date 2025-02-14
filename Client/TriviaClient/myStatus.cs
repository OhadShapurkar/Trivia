using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriviaClient
{
    public partial class myStatus : Form
    {
        private NetworkStream clientStream;
        private string username;

        public myStatus(NetworkStream clientStream2, string username2)
        {
            InitializeComponent();
            username = username2;
            clientStream = clientStream2;
        }

        private void myStatus_Load(object sender, EventArgs e)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("225");
            clientStream.Write(buffer, 0, 3);
            clientStream.Flush();
            string input;
            buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            input = new ASCIIEncoding().GetString(buffer);
            if ((input.Substring(0, 3)).Equals("126"))
            {
                string bestScores = "";
                int val = int.Parse(input.Substring(3, 4));
                bestScores += "Number of games: " + val.ToString();
                val = int.Parse(input.Substring(7, 6));
                bestScores += "\nNumber of correct answers: " + val.ToString();
                val = int.Parse(input.Substring(13, 6));
                bestScores += "\nNumber of wrong answers: " + val.ToString();
                val = int.Parse(input.Substring(19, 2));
                bestScores += "\nAverage time for each question: " + val.ToString() + ".";
                val = int.Parse(input.Substring(21, 2));
                bestScores += val.ToString();
                status.Text = bestScores;
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            MenuForm wnd = new MenuForm(clientStream, username);
            wnd.ShowDialog();
            
        }

        private void myStatus_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            
        }

    }
}
