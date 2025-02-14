namespace TriviaClient
{
    partial class waitForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.closeOrLeaveRoom = new System.Windows.Forms.Button();
            this.startGame = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TriviaClient.Properties.Resources.Cool_Text___Trivia_249045623498474;
            this.pictureBox1.Location = new System.Drawing.Point(194, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 110);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(125, 155);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(375, 134);
            this.listBox1.TabIndex = 4;
            // 
            // closeOrLeaveRoom
            // 
            this.closeOrLeaveRoom.BackColor = System.Drawing.Color.Teal;
            this.closeOrLeaveRoom.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.closeOrLeaveRoom.ForeColor = System.Drawing.Color.AliceBlue;
            this.closeOrLeaveRoom.Location = new System.Drawing.Point(205, 316);
            this.closeOrLeaveRoom.Name = "closeOrLeaveRoom";
            this.closeOrLeaveRoom.Size = new System.Drawing.Size(224, 65);
            this.closeOrLeaveRoom.TabIndex = 9;
            this.closeOrLeaveRoom.Text = "Leave Room";
            this.closeOrLeaveRoom.UseVisualStyleBackColor = false;
            this.closeOrLeaveRoom.Click += new System.EventHandler(this.closeOrLeaveRoom_Click);
            // 
            // startGame
            // 
            this.startGame.BackColor = System.Drawing.Color.Teal;
            this.startGame.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.startGame.ForeColor = System.Drawing.Color.AliceBlue;
            this.startGame.Location = new System.Drawing.Point(205, 396);
            this.startGame.Name = "startGame";
            this.startGame.Size = new System.Drawing.Size(224, 65);
            this.startGame.TabIndex = 10;
            this.startGame.Text = "Start Game";
            this.startGame.UseVisualStyleBackColor = false;
            this.startGame.Visible = false;
            this.startGame.Click += new System.EventHandler(this.startGame_Click);
            // 
            // waitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(634, 473);
            this.Controls.Add(this.startGame);
            this.Controls.Add(this.closeOrLeaveRoom);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "waitForm";
            this.Text = "waitForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.waitForm_FormClosing);
            this.Load += new System.EventHandler(this.waitForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button closeOrLeaveRoom;
        private System.Windows.Forms.Button startGame;
    }
}