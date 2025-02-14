using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriviaClient
{
    public partial class LoginForm : Form
    {

        private NetworkStream clientStream;

        public LoginForm()
        {
            InitializeComponent();
        }

        public LoginForm(NetworkStream clientStream2)
        {
            InitializeComponent();
            clientStream = clientStream2;
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            if (clientStream == null)
            {
                Thread thread = new Thread(socket);
                thread.Start();
            }
        }

        private void socket()
        {
            try
            {
                TcpClient client = new TcpClient();
                IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8820);

                client.Connect(serverEndPoint);
                clientStream = client.GetStream();
                
                byte[] temp2 = new ASCIIEncoding().GetBytes("1020");
            }
            catch (Exception ex)
            {
                Invoke((MethodInvoker)delegate { MessageBox.Show("Server is not running. Please try again!"); });
                Hide();
                Application.Exit();
            }
        }

        private void Login_Click(object sender, EventArgs e)
        {
            string userLength = (Username.Text.ToString().Length > 9) ? (Username.Text.ToString().Length).ToString() : ('0' + (Username.Text.ToString().Length).ToString());
            string passLength = (Password.Text.ToString().Length > 9) ? (Password.Text.ToString().Length).ToString() : ('0' + (Password.Text.ToString().Length).ToString());
            string message = "200" + userLength + Username.Text.ToString() + passLength + Password.Text.ToString();
            clientStream.Write(Encoding.UTF8.GetBytes(message), 0, message.Length);
            clientStream.Flush();
            string input;
            byte[] buffer = new byte[4];
            int bytesRead = clientStream.Read(buffer, 0, 4);
            input = new ASCIIEncoding().GetString(buffer);
            if (input.Equals("1020"))
            {
                Hide();
                MenuForm wnd = new MenuForm(clientStream, Username.Text);
                wnd.ShowDialog();
                ;
                try
                {
                    buffer = new ASCIIEncoding().GetBytes("299");
                    clientStream.Write(buffer, 0, 3);
                    clientStream.Flush();
                    Hide();
                }
                catch
                { }
                Application.Exit();

            }
            else if(input.Equals("1021"))
            {
                label4.Text = "Username or password incorrect. Please try again";
            }
            else // 1022
            {
                label4.Text = "This user already signed in";
            }
        }

        private void Register_Click(object sender, EventArgs e)
        {
            Hide();
            RegisterForm wnd = new RegisterForm(clientStream);
            wnd.ShowDialog();
            try
            {
                byte[] buffer = new ASCIIEncoding().GetBytes("299");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
                Hide();
            }
            catch
            { }
            Application.Exit();
        }

        private void Quit_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] buffer = new ASCIIEncoding().GetBytes("299");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
                Hide();
            }
            catch
            { }
            Hide();
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                byte[] buffer = new ASCIIEncoding().GetBytes("299");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
                Hide();
            }
            catch
            { }
            Hide();
            
        }
    }
}
