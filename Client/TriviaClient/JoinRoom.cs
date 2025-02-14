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
    public partial class JoinRoom : Form
    {
        Dictionary<string, string> roomList = new Dictionary<string, string>();
        private NetworkStream clientStream;
        private string username;

        public JoinRoom(NetworkStream clientStream2, string username2)
        {
            InitializeComponent();
            clientStream = clientStream2;
            username = username2;
        }

        private void Back_Click(object sender, EventArgs e)
        {
            Hide();
            
            MenuForm wnd = new MenuForm(clientStream, username);
            wnd.ShowDialog();
        }

        private void LoadRooms()
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("205");
            clientStream.Write(buffer, 0, 3);
            clientStream.Flush();
            buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            string input = new ASCIIEncoding().GetString(buffer);
            if ((input.Substring(0, 3)).Equals("106"))
            {
                listBox1.Items.Clear();
                roomList.Clear();
                int numberOfRooms = int.Parse(input.Substring(3, 4));
                input = input.Remove(0, 7);
                for (int i = 0; i < numberOfRooms; i++)
                {
                    string roomId = input.Substring(0, 4);
                    int roomNameLength = int.Parse(input.Substring(4, 2));
                    string roomName = input.Substring(6, roomNameLength);
                    roomList[roomName] = roomId;
                    listBox1.Items.Add(roomName);
                }
            }
        }

        private void loadPlayersInRoom(string roomId)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes(("207" + roomId));
            clientStream.Write(buffer, 0, 7);
            clientStream.Flush();
            buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            string input = new ASCIIEncoding().GetString(buffer);
            if ((input.Substring(0, 3)).Equals("108"))
            {
                listBox2.Items.Clear();
                int numberOfUsers = int.Parse(input.Substring(3, 1));
                input = input.Remove(0, 4);
                for (int i = 0; i < numberOfUsers; i++)
                {
                    int userNameLength = int.Parse(input.Substring(0, 2));
                    string userName = input.Substring(2, userNameLength);
                    listBox2.Items.Add(userName);
                    input = input.Remove(0, userNameLength + 2);
                }
            }
        }

        private void joinRoomById(string roomId)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes(("209" + roomId));
            clientStream.Write(buffer, 0, 7);
            clientStream.Flush();
            buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            string input = new ASCIIEncoding().GetString(buffer);
            if (input[3]=='0')
            {
                int questionsNumber = int.Parse(input.Substring(4, 2));
                int questionTimeInSec = int.Parse(input.Substring(6, 2));
                Hide();
                waitForm wnd = new waitForm(clientStream, false, questionTimeInSec, username);
                wnd.ShowDialog();
                
            }
        }

        private void JoinRoom_Load(object sender, EventArgs e)
        {
            LoadRooms();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadRooms();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                joinRoomById(roomList[listBox1.SelectedItem.ToString()]);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string selectedRoom = listBox1.SelectedItem.ToString();
                loadPlayersInRoom(roomList[selectedRoom]);
            }
        }

        private void JoinRoom_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            
        }
    }
}
