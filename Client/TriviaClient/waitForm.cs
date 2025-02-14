using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TriviaClient
{
    public partial class waitForm : Form
    {
        private NetworkStream clientStream;
        private bool isAdmin;
        private int time;
        private bool stillRunning = true;
        private Thread thread;
        private string username;

        public waitForm(NetworkStream clientStream2, bool isAdmin2, int time2, string username2)
        {
            InitializeComponent();
            clientStream = clientStream2;
            isAdmin = isAdmin2;
            time = time2;
            username = username2;
            thread = new Thread(updateRoomUsers);
            thread.Start();
            if (isAdmin)
            {
                listBox1.Items.Add(username);
                startGame.Visible = true;
                closeOrLeaveRoom.Text = "Close Room";
            }
        }

        private void updateRoomUsers()
        {
            try
            {
                while (stillRunning)
                {
                    byte[] buffer = new byte[3];
                    int bytesRead = clientStream.Read(buffer, 0, 3);
                    string input = new ASCIIEncoding().GetString(buffer);
                    if (input.Substring(0,3).Equals("108"))
                    {
                        buffer = new byte[256];
                        bytesRead = clientStream.Read(buffer, 0, 256);
                        input = new ASCIIEncoding().GetString(buffer);
                        Invoke((MethodInvoker)delegate { listBox1.Items.Clear(); });
                        int numberOfUsers = int.Parse(input.Substring(0, 1));
                        input = input.Remove(0, 1);
                        for (int i = 0; i < numberOfUsers; i++)
                        {
                            int userNameLength = int.Parse(input.Substring(0, 2));
                            string userName = input.Substring(2, userNameLength);
                            Invoke((MethodInvoker)delegate { listBox1.Items.Add(userName); });
                            input = input.Remove(0, userNameLength + 2);
                        }
                    }
                    else if(input.Substring(0,3).Equals("118"))
                    {
                        if(!isAdmin)
                        {
                            Invoke((MethodInvoker)delegate {
                                Hide();
                                Game wnd = new Game(clientStream, time, username);
                                wnd.ShowDialog();
                                
                            });
                        }
                        stillRunning = false;
                    }
                    else if(input.Substring(0,3).Equals("116"))
                    {
                        Invoke((MethodInvoker)delegate
                        {
                            Hide();
                            MenuForm wnd = new MenuForm(clientStream, username);
                            wnd.ShowDialog();
                            
                        });
                    }
                    else if (input.Equals("112"))
                    {
                        buffer = new byte[1];
                        bytesRead = clientStream.Read(buffer, 0, 1);
                        Invoke((MethodInvoker)delegate
                        {
                            Hide();
                            MenuForm wnd = new MenuForm(clientStream, username);
                            wnd.ShowDialog();
                            
                        });
                    }
                }

            }
            catch
            {

            }
        }


        private void waitForm_Load(object sender, EventArgs e)
        {
            /*string input;
            byte[] buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            input = new ASCIIEncoding().GetString(buffer);
            int startPoint = 4;
            if ((input.Substring(0, 3)).Equals("108") && (!(input.Substring(3, 1).Equals("0"))))
            {
                for (int i = 0; i < int.Parse(input.Substring(3, 1)); i++)
                {
                    int userLength = int.Parse(input.Substring(startPoint, 2));
                    listBox1.Items.Add(input.Substring(startPoint + 2, userLength));
                    startPoint += userLength + 3;
                }
            }
            else if ((input.Substring(3, 1).Equals("0")))
            {
                stillRunning = false;
                buffer = new ASCIIEncoding().GetBytes("299");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
                Hide();
            }*/
            //thread = new Thread(updateRoomUsers);
            //thread.Start();
        }

        private void startGame_Click(object sender, EventArgs e)
        {
            stillRunning = false;
            byte[] buffer = new ASCIIEncoding().GetBytes("217");
            clientStream.Write(buffer, 0, 3);
            clientStream.Flush();
            Hide();
            Game wnd = new Game(clientStream, time, username);
            wnd.ShowDialog();
            

        }

        private void closeOrLeaveRoom_Click(object sender, EventArgs e)
        {
            if (isAdmin)
            {
                byte[] buffer = new ASCIIEncoding().GetBytes("215");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
            }
            else
            {
                byte[] buffer = new ASCIIEncoding().GetBytes("211");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
            }
        }

        private void waitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            byte[] buffer = new byte[256];
            stillRunning = false;
            if (isAdmin)
            {
                buffer = new ASCIIEncoding().GetBytes("215");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
            }
            else
            {
                buffer = new ASCIIEncoding().GetBytes("211");
                clientStream.Write(buffer, 0, 3);
                clientStream.Flush();
            }
        }

    }
}
