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
    public partial class BestScores : Form
    {
        private NetworkStream clientStream;
        private string username;
        public BestScores(NetworkStream clientStream2, string username2)
        {
            InitializeComponent();
            clientStream = clientStream2;
            username = username2;
        }

        private void BestScores_Load(object sender, EventArgs e)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("223");
            clientStream.Write(buffer, 0, 3);
            clientStream.Flush();
            string input;
            buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            input = new ASCIIEncoding().GetString(buffer);
            if((input.Substring(0,3)).Equals("124"))
            {
                int startPoint = 3;
                string bestScores = "";
                for (int i = 0; i < 3; i++)
                {
                    int val = int.Parse(input.Substring(startPoint, 2));
                    bestScores += input.Substring(startPoint + 2, val) + " - ";
                    int oldVal;
                    if (val == 0)
                    {
                        oldVal = val + startPoint;
                    }
                    else
                    {
                        oldVal = val + 2 + startPoint;
                    }
                    val = Int32.Parse(input.Substring(oldVal, 6));
                    bestScores += val.ToString() + "\n";
                    if(val == 0)
                    {
                        startPoint = oldVal + 1;
                    }
                    else
                    {
                        startPoint = oldVal + 6;
                    }
                }
                scores.Text = bestScores;
            }
            
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            MenuForm wnd = new MenuForm(clientStream, username);
            wnd.ShowDialog();
            
        }

        private void BestScores_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            
        }
    }
}
