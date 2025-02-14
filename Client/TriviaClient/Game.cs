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
    public partial class Game : Form
    {
        private int originalTime;
        private int timeLeft;
        private NetworkStream clientStream;
        private string answer = "219";
        private string username;

        public Game(NetworkStream clientStream2, int time2, string username2)
        {
            InitializeComponent();
            clientStream = clientStream2;
            timeLeft = time2;
            originalTime = time2;
            Time.Text = originalTime.ToString();
            username = username2;
            string input;
            byte[] buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            input = new ASCIIEncoding().GetString(buffer);
            int startPoint = 3;
            int Length = int.Parse(input.Substring(0, 3));
            question.Text = input.Substring(startPoint, Length);
            startPoint += Length;

            for (int i = 0; i < 4; i++)
            {
                Length = int.Parse(input.Substring(startPoint, 3));
                startPoint += 3;
                if (i == 0) { answer1.Text = input.Substring(startPoint, Length); }
                else if (i == 1) { answer2.Text = input.Substring(startPoint, Length); }
                else if (i == 2) { answer3.Text = input.Substring(startPoint, Length); }
                else { answer4.Text = input.Substring(startPoint, Length); }
                startPoint += Length;
            }
            timer1.Start();
        }

        private void Game_Load(object sender, EventArgs e)
        {

        }


        private void answer1_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            answer += "1";
            answer += (originalTime - timeLeft > 9) ? (originalTime - timeLeft).ToString() : ('0' + (originalTime - timeLeft).ToString());
            HandleAnswers();
        }

        private void answer2_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            answer += "2";
            answer += (originalTime - timeLeft > 9) ? (originalTime - timeLeft).ToString() : ('0' + (originalTime - timeLeft).ToString());
            HandleAnswers();
        }

        private void answer3_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            answer += "3";
            answer += (originalTime - timeLeft > 9) ? (originalTime - timeLeft).ToString() : ('0' + (originalTime - timeLeft).ToString());
            HandleAnswers();
        }

        private void answer4_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            answer += "4";
            answer += (originalTime - timeLeft > 9) ? (originalTime - timeLeft).ToString() : ('0' + (originalTime - timeLeft).ToString());
            HandleAnswers();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                timeLeft = timeLeft - 1;
                Time.Text = timeLeft.ToString();
            }
            else
            {
                timer1.Stop();
                HandleAnswers();
            }
            
        }

        private void HandleAnswers()
        {
            if(answer.Length == 3)
            {
                answer += "510";
            }
            byte[] buffer = new ASCIIEncoding().GetBytes(answer);
            clientStream.Write(buffer, 0, answer.Length);
            clientStream.Flush();
            buffer = new byte[256];
            string input;
            int bytesRead = clientStream.Read(buffer, 0, 256);
            input = new ASCIIEncoding().GetString(buffer);
            if((input.Substring(0, 3)).Equals("120"))
            {
                if(input[3] == '1')
                {
                    if (answer[3] == '1') { answer1.BackColor = Color.Green; } else { answer1.BackColor = Color.Red; };
                    if (answer[3] == '2') { answer2.BackColor = Color.Green; } else { answer2.BackColor = Color.Red; };
                    if (answer[3] == '3') { answer3.BackColor = Color.Green; } else { answer3.BackColor = Color.Red; };
                    if (answer[3] == '4') { answer4.BackColor = Color.Green; } else { answer4.BackColor = Color.Red; };
                }
                else
                {
                    answer1.BackColor = Color.Red;
                    answer2.BackColor = Color.Red;
                    answer3.BackColor = Color.Red;
                    answer4.BackColor = Color.Red;
                }
                answer1.Refresh();
                answer2.Refresh();
                answer3.Refresh();
                answer4.Refresh();
            }
            resetAndUpdate();
        }

        private void resetAndUpdate()
        {
            System.Threading.Thread.Sleep(1000);
            Time.Text = originalTime.ToString();
            answer1.BackColor = Color.LightBlue;
            answer2.BackColor = Color.LightBlue;
            answer3.BackColor = Color.LightBlue;
            answer4.BackColor = Color.LightBlue;
            timeLeft = originalTime;
            answer = "219";
            string input;
            byte[] buffer = new byte[256];
            int bytesRead = clientStream.Read(buffer, 0, 256);
            input = new ASCIIEncoding().GetString(buffer);
            int startPoint = 6;
            if ((input.Substring(0, 3)).Equals("118") && (input.Length != 4))
            {
                int Length = int.Parse(input.Substring(3, 3));
                question.Text = input.Substring(startPoint, Length);
                startPoint += Length;

                for (int i = 0; i < 4; i++)
                {
                    Length = int.Parse(input.Substring(startPoint, 3));
                    startPoint += 3;
                    if (i == 0) { answer1.Text = input.Substring(startPoint, Length); }
                    else if (i == 1) { answer2.Text = input.Substring(startPoint, Length); }
                    else if (i == 2) { answer3.Text = input.Substring(startPoint, Length); }
                    else { answer4.Text = input.Substring(startPoint, Length); }
                    startPoint += Length;
                }
                timer1.Start();
            }
            else if((input.Substring(0, 3)).Equals("121"))
            {
                startPoint = 4;
                string score = "";
                for (int i = 0; i < int.Parse(input.Substring(3, 1)); i++)
                {
                    int Length = int.Parse(input.Substring(startPoint, 2));
                    startPoint += 2;
                    score += input.Substring(startPoint, Length) + " - ";
                    startPoint += Length;
                    score += int.Parse(input.Substring(startPoint, 2)).ToString() + " Points\n";
                    startPoint += 2;
                }
                MessageBox.Show(score);
                Hide();
                MenuForm wnd = new MenuForm(clientStream, username);
                wnd.ShowDialog();
                
            }
        }

        private void quitGame_Click(object sender, EventArgs e)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("222");
            clientStream.Write(buffer, 0, 3);
            clientStream.Flush();
            Hide();
            MenuForm wnd = new MenuForm(clientStream, username);
            wnd.ShowDialog();
            

        }

        private void Game_FormClosing(object sender, FormClosingEventArgs e)
        {
            byte[] buffer = new ASCIIEncoding().GetBytes("222");
            clientStream.Write(buffer, 0, 3);
            clientStream.Flush();
            Hide();
            
        }
    }
}
