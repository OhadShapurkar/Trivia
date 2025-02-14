namespace TriviaClient
{
    partial class MenuForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.JoinRoom = new System.Windows.Forms.Button();
            this.CreateRoom = new System.Windows.Forms.Button();
            this.MyStatus = new System.Windows.Forms.Button();
            this.BestScores = new System.Windows.Forms.Button();
            this.Quit = new System.Windows.Forms.Button();
            this.SignOut = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // JoinRoom
            // 
            this.JoinRoom.BackColor = System.Drawing.Color.Teal;
            this.JoinRoom.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.JoinRoom.ForeColor = System.Drawing.Color.AliceBlue;
            this.JoinRoom.Location = new System.Drawing.Point(215, 140);
            this.JoinRoom.Name = "JoinRoom";
            this.JoinRoom.Size = new System.Drawing.Size(210, 60);
            this.JoinRoom.TabIndex = 1;
            this.JoinRoom.Text = "Join Room";
            this.JoinRoom.UseVisualStyleBackColor = false;
            this.JoinRoom.Click += new System.EventHandler(this.JoinRoom_Click);
            // 
            // CreateRoom
            // 
            this.CreateRoom.BackColor = System.Drawing.Color.Teal;
            this.CreateRoom.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateRoom.ForeColor = System.Drawing.Color.AliceBlue;
            this.CreateRoom.Location = new System.Drawing.Point(215, 215);
            this.CreateRoom.Name = "CreateRoom";
            this.CreateRoom.Size = new System.Drawing.Size(210, 60);
            this.CreateRoom.TabIndex = 2;
            this.CreateRoom.Text = "Create Room";
            this.CreateRoom.UseVisualStyleBackColor = false;
            this.CreateRoom.Click += new System.EventHandler(this.CreateRoom_Click);
            // 
            // MyStatus
            // 
            this.MyStatus.BackColor = System.Drawing.Color.Teal;
            this.MyStatus.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MyStatus.ForeColor = System.Drawing.Color.AliceBlue;
            this.MyStatus.Location = new System.Drawing.Point(215, 291);
            this.MyStatus.Name = "MyStatus";
            this.MyStatus.Size = new System.Drawing.Size(210, 60);
            this.MyStatus.TabIndex = 3;
            this.MyStatus.Text = "My Status";
            this.MyStatus.UseVisualStyleBackColor = false;
            this.MyStatus.Click += new System.EventHandler(this.MyStatus_Click);
            // 
            // BestScores
            // 
            this.BestScores.BackColor = System.Drawing.Color.Teal;
            this.BestScores.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BestScores.ForeColor = System.Drawing.Color.AliceBlue;
            this.BestScores.Location = new System.Drawing.Point(215, 366);
            this.BestScores.Name = "BestScores";
            this.BestScores.Size = new System.Drawing.Size(210, 60);
            this.BestScores.TabIndex = 4;
            this.BestScores.Text = "Best Scores";
            this.BestScores.UseVisualStyleBackColor = false;
            this.BestScores.Click += new System.EventHandler(this.BestScores_Click);
            // 
            // Quit
            // 
            this.Quit.BackColor = System.Drawing.Color.Teal;
            this.Quit.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Quit.ForeColor = System.Drawing.Color.AliceBlue;
            this.Quit.Location = new System.Drawing.Point(468, 434);
            this.Quit.Name = "Quit";
            this.Quit.Size = new System.Drawing.Size(154, 27);
            this.Quit.TabIndex = 5;
            this.Quit.Text = "Quit";
            this.Quit.UseVisualStyleBackColor = false;
            this.Quit.Click += new System.EventHandler(this.Quit_Click);
            // 
            // SignOut
            // 
            this.SignOut.BackColor = System.Drawing.Color.Teal;
            this.SignOut.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SignOut.ForeColor = System.Drawing.Color.AliceBlue;
            this.SignOut.Location = new System.Drawing.Point(12, 434);
            this.SignOut.Name = "SignOut";
            this.SignOut.Size = new System.Drawing.Size(154, 27);
            this.SignOut.TabIndex = 6;
            this.SignOut.Text = "Sign Out";
            this.SignOut.UseVisualStyleBackColor = false;
            this.SignOut.Click += new System.EventHandler(this.SignOut_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TriviaClient.Properties.Resources.Cool_Text___Trivia_249045623498474;
            this.pictureBox1.Location = new System.Drawing.Point(200, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 110);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // MenuForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(634, 473);
            this.Controls.Add(this.SignOut);
            this.Controls.Add(this.Quit);
            this.Controls.Add(this.BestScores);
            this.Controls.Add(this.MyStatus);
            this.Controls.Add(this.CreateRoom);
            this.Controls.Add(this.JoinRoom);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MenuForm";
            this.Text = "Menu";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MenuForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button JoinRoom;
        private System.Windows.Forms.Button CreateRoom;
        private System.Windows.Forms.Button MyStatus;
        private System.Windows.Forms.Button BestScores;
        private System.Windows.Forms.Button Quit;
        private System.Windows.Forms.Button SignOut;
    }
}