namespace chess
{
    partial class Window
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
            this.Chess = new System.Windows.Forms.Panel();
            this.Play = new System.Windows.Forms.Button();
            this.Restart = new System.Windows.Forms.Button();
            this.Turn_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Chess
            // 
            this.Chess.Location = new System.Drawing.Point(104, 47);
            this.Chess.Name = "Chess";
            this.Chess.Size = new System.Drawing.Size(399, 117);
            this.Chess.TabIndex = 17;
            // 
            // Play
            // 
            this.Play.BackColor = System.Drawing.Color.ForestGreen;
            this.Play.FlatAppearance.BorderSize = 0;
            this.Play.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Play.Font = new System.Drawing.Font("Comic Sans MS", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Play.Location = new System.Drawing.Point(12, 12);
            this.Play.Name = "Play";
            this.Play.Size = new System.Drawing.Size(80, 60);
            this.Play.TabIndex = 14;
            this.Play.Text = "Play!";
            this.Play.UseVisualStyleBackColor = false;
            this.Play.Click += new System.EventHandler(this.Play_Click);
            // 
            // Restart
            // 
            this.Restart.BackColor = System.Drawing.Color.Red;
            this.Restart.FlatAppearance.BorderSize = 0;
            this.Restart.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.Restart.Font = new System.Drawing.Font("Comic Sans MS", 7F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Restart.Location = new System.Drawing.Point(12, 114);
            this.Restart.Name = "Restart";
            this.Restart.Size = new System.Drawing.Size(70, 50);
            this.Restart.TabIndex = 18;
            this.Restart.Text = "Restart";
            this.Restart.UseVisualStyleBackColor = false;
            this.Restart.Click += new System.EventHandler(this.Restart_Click);
            // 
            // Turn_label
            // 
            this.Turn_label.AutoSize = true;
            this.Turn_label.Font = new System.Drawing.Font("Comic Sans MS", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Turn_label.Location = new System.Drawing.Point(98, 12);
            this.Turn_label.Name = "Turn_label";
            this.Turn_label.Size = new System.Drawing.Size(0, 24);
            this.Turn_label.TabIndex = 20;
            // 
            // Window
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 23F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(678, 544);
            this.Controls.Add(this.Turn_label);
            this.Controls.Add(this.Restart);
            this.Controls.Add(this.Chess);
            this.Controls.Add(this.Play);
            this.Font = new System.Drawing.Font("Comic Sans MS", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(0, -150);
            this.MaximizeBox = false;
            this.Name = "Window";
            this.ShowIcon = false;
            this.Text = "chess";
            this.Load += new System.EventHandler(this.Window_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel Chess;
        private System.Windows.Forms.Button Play;
        private System.Windows.Forms.Button Restart;
        private System.Windows.Forms.Label Turn_label;
    }
}

