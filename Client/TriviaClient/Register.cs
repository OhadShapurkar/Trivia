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
    public partial class RegisterForm : Form
    {
        private NetworkStream clientStream;
        public RegisterForm(NetworkStream clientStream2)
        {
            InitializeComponent();
            clientStream = clientStream2;
        }

        private void Register_Click(object sender, EventArgs e)
        {
            if (Username.Text.ToString().Length == 0 || Password.Text.ToString().Length == 0 || Email.Text.ToString().Length == 0)
            {
                label5.Text = "Please fill out all fields!";
                return;
            }
            string userLength = (Username.Text.ToString().Length > 9) ? (Username.Text.ToString().Length).ToString() : ('0' + (Username.Text.ToString().Length).ToString());
            string passLength = (Password.Text.ToString().Length > 9) ? (Password.Text.ToString().Length).ToString() : ('0' + (Password.Text.ToString().Length).ToString());
            string emailLength = (Email.Text.ToString().Length > 9) ? (Email.Text.ToString().Length).ToString() : ('0' + (Email.Text.ToString().Length).ToString());
            string message = "203" + userLength + Username.Text.ToString() + passLength + Password.Text.ToString() + emailLength + Email.Text.ToString();
            clientStream.Write(Encoding.UTF8.GetBytes(message), 0, message.Length);
            clientStream.Flush();
            string input;
            byte[] buffer = new byte[4];
            int bytesRead = clientStream.Read(buffer, 0, 4);
            input = new ASCIIEncoding().GetString(buffer);
            if(input.Equals("1040"))
            {
                MessageBox.Show("You've registered succuesfully, Please Login");
                Hide();
                LoginForm wnd = new LoginForm();
                wnd.ShowDialog();
                

            }
            else if (input.Equals("1041"))
            {
                label5.Text = "password is illigal: 4 digit, digit lower,\n upper case, no spaces";
            }
            else if (input.Equals("1042"))
            {
                label5.Text = "This username is already exists";
            }
            else if (input.Equals("1043"))
            {
                label5.Text = "username is illigal: not empty, no spaces,\nstart with letter";
            }
            else
            {
                label5.Text = "Error, Please try again later";
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm wnd = new LoginForm();
            wnd.ShowDialog();
            
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            Hide();
        }

        private void RegisterForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
        }
    }
}
