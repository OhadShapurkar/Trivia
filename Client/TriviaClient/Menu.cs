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
    public partial class MenuForm : Form
    {
        private NetworkStream clientStream;
        private string username;

        public MenuForm(NetworkStream clientStream2, string username2)
        {
            InitializeComponent();
            clientStream = clientStream2;
            username = username2;
        }

        private void SignOut_Click(object sender, EventArgs e)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("201");
            clientStream.Write(buffer, 0, 3);
            clientStream.Flush();
            Hide();
            LoginForm wnd = new LoginForm(clientStream);
            wnd.ShowDialog();
            
        }

        private void MyStatus_Click(object sender, EventArgs e)
        {
            Hide();
            myStatus wnd = new myStatus(clientStream, username);
            wnd.ShowDialog();
            
        }

        private void BestScores_Click(object sender, EventArgs e)
        {
            Hide();
            BestScores wnd = new BestScores(clientStream, username);
            wnd.ShowDialog();
            
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void MenuForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
        }

        private void JoinRoom_Click(object sender, EventArgs e)
        {
            Hide();
            JoinRoom wnd = new JoinRoom(clientStream, username);
            wnd.ShowDialog();
        }

        private void CreateRoom_Click(object sender, EventArgs e)
        {
            Hide();
            CreateRoom wnd = new CreateRoom(clientStream, username);
            wnd.ShowDialog();
        }
    }
}
