using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;

namespace TriviaClient
{
    public partial class CreateRoom : Form
    {
        string username;
        private NetworkStream clientStream;

        public CreateRoom(NetworkStream clientStream2, string username2)
        {
            InitializeComponent();
            clientStream = clientStream2;
            username = username2;
        }

        private void Back_Click_1(object sender, EventArgs e)
        {
            Hide();
            MenuForm wnd = new MenuForm(clientStream, username);
            wnd.ShowDialog();
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string message = "213" + (textBox_roomName.Text.Length < 10 ? "0" + textBox_roomName.Text.Length.ToString() : textBox_roomName.Text.Length.ToString()) + textBox_roomName.Text + textBox_playersNum.Text + (textBox_questionsNum.Text.Length < 2 ? "0" + textBox_questionsNum.Text : textBox_questionsNum.Text) + (textBox_questionTime.Text.Length < 2 ? "0" + textBox_questionTime.Text : textBox_questionTime.Text);
            byte[] buffer = new ASCIIEncoding().GetBytes(message);
            clientStream.Write(buffer, 0, buffer.Length);
            clientStream.Flush();
            buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0,  256);
            string input = new ASCIIEncoding().GetString(buffer);
            if (input.Substring(0, 4)=="1140")
            {
                Hide();
                waitForm wnd = new waitForm(clientStream, true, int.Parse(textBox_questionTime.Text), username);
                wnd.ShowDialog();
                
            }
            else
            {
                label2.Text = "Error!";
            }
        }

        private void CreateRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            
        }

    }
}
