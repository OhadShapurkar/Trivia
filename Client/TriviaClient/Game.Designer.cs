namespace TriviaClient
{
    partial class Game
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
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.question = new System.Windows.Forms.Label();
            this.answer4 = new System.Windows.Forms.Label();
            this.answer3 = new System.Windows.Forms.Label();
            this.answer2 = new System.Windows.Forms.Label();
            this.answer1 = new System.Windows.Forms.Label();
            this.quitGame = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TriviaClient.Properties.Resources.Cool_Text___Trivia_249045623498474;
            this.pictureBox1.Location = new System.Drawing.Point(194, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(250, 110);
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // question
            // 
            this.question.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.question.Location = new System.Drawing.Point(75, 134);
            this.question.Name = "question";
            this.question.Size = new System.Drawing.Size(500, 70);
            this.question.TabIndex = 4;
            this.question.Text = "Question";
            this.question.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // answer4
            // 
            this.answer4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.answer4.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answer4.Location = new System.Drawing.Point(75, 374);
            this.answer4.Name = "answer4";
            this.answer4.Size = new System.Drawing.Size(500, 36);
            this.answer4.TabIndex = 6;
            this.answer4.Text = "D";
            this.answer4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.answer4.Click += new System.EventHandler(this.answer4_Click);
            // 
            // answer3
            // 
            this.answer3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.answer3.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answer3.Location = new System.Drawing.Point(75, 327);
            this.answer3.Name = "answer3";
            this.answer3.Size = new System.Drawing.Size(500, 36);
            this.answer3.TabIndex = 7;
            this.answer3.Text = "C";
            this.answer3.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.answer3.Click += new System.EventHandler(this.answer3_Click);
            // 
            // answer2
            // 
            this.answer2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.answer2.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answer2.Location = new System.Drawing.Point(75, 278);
            this.answer2.Name = "answer2";
            this.answer2.Size = new System.Drawing.Size(500, 36);
            this.answer2.TabIndex = 8;
            this.answer2.Text = "B";
            this.answer2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.answer2.Click += new System.EventHandler(this.answer2_Click);
            // 
            // answer1
            // 
            this.answer1.BackColor = System.Drawing.Color.LightBlue;
            this.answer1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.answer1.Font = new System.Drawing.Font("Comic Sans MS", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.answer1.Location = new System.Drawing.Point(75, 224);
            this.answer1.Name = "answer1";
            this.answer1.Size = new System.Drawing.Size(500, 36);
            this.answer1.TabIndex = 9;
            this.answer1.Text = "A";
            this.answer1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.answer1.Click += new System.EventHandler(this.answer1_Click);
            // 
            // quitGame
            // 
            this.quitGame.BackColor = System.Drawing.Color.Teal;
            this.quitGame.Font = new System.Drawing.Font("Comic Sans MS", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.quitGame.ForeColor = System.Drawing.Color.AliceBlue;
            this.quitGame.Location = new System.Drawing.Point(468, 434);
            this.quitGame.Name = "quitGame";
            this.quitGame.Size = new System.Drawing.Size(154, 27);
            this.quitGame.TabIndex = 10;
            this.quitGame.Text = "Quit Game";
            this.quitGame.UseVisualStyleBackColor = false;
            this.quitGame.Click += new System.EventHandler(this.quitGame_Click);
            // 
            // Time
            // 
            this.Time.BackColor = System.Drawing.Color.PowderBlue;
            this.Time.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Time.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Time.Location = new System.Drawing.Point(556, 12);
            this.Time.Name = "Time";
            this.Time.Size = new System.Drawing.Size(66, 40);
            this.Time.TabIndex = 11;
            this.Time.Text = "0";
            this.Time.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightBlue;
            this.ClientSize = new System.Drawing.Size(634, 473);
            this.Controls.Add(this.Time);
            this.Controls.Add(this.quitGame);
            this.Controls.Add(this.answer1);
            this.Controls.Add(this.answer2);
            this.Controls.Add(this.answer3);
            this.Controls.Add(this.answer4);
            this.Controls.Add(this.question);
            this.Controls.Add(this.pictureBox1);
            this.Name = "Game";
            this.Text = "Game";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Game_FormClosing);
            this.Load += new System.EventHandler(this.Game_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label question;
        private System.Windows.Forms.Label answer4;
        private System.Windows.Forms.Label answer3;
        private System.Windows.Forms.Label answer2;
        private System.Windows.Forms.Label answer1;
        private System.Windows.Forms.Button quitGame;
        private System.Windows.Forms.Label Time;
        private System.Windows.Forms.Timer timer1;
    }
}