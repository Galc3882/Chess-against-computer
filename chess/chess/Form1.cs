using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace chess
{
    public partial class Window : Form
    {
        Button[,] b = new Button[8, 8];
        int[,] a = new int[8, 8];
        int r, c, k, r2, c2;

        Image b_pawn = Properties.Resources.b_pawn;
        Image w_pawn = Properties.Resources.w_pawn;
        Image b_rook = Properties.Resources.b_rook;
        Image w_rook = Properties.Resources.w_rook;
        Image b_knight = Properties.Resources.b_knight;
        Image w_knight = Properties.Resources.w_knight;
        Image b_bishop = Properties.Resources.b_bishop;
        Image w_bishop = Properties.Resources.w_bishop;
        Image b_queen = Properties.Resources.b_queen;
        Image w_queen = Properties.Resources.w_queen;
        Image b_king = Properties.Resources.b_king;
        Image w_king = Properties.Resources.w_king;

        public Window()
        {
            InitializeComponent();
        }

        private void Window_Load(object sender, EventArgs e)
        {

        }

        static void Clean(Button[,] b)
        {
            int r, c;
            for (r = 0; r < 8; r++)
            {
                for (c = 0; c < 8; c++)
                {
                    b[r, c].ForeColor = Color.Black;
                    b[r, c].FlatAppearance.BorderSize = 1;
                }
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Play.Enabled = false;
            Turn_label.Text = "Turn: White"; //show turn
            int z = 0;
            Chess.Height = 448;
            Chess.Width = 448;
            this.Height = Chess.Height + 130; //window size
            this.Width = Chess.Width + 170; //window size
            for (r = 0; r < 8; r++)
            {
                for (c = 0; c < 8; c++)
                {
                    b[r, c] = new Button();
                    b[r, c].Size = new Size(56, 56);
                    b[r, c].Location = new Point(c * 56, r * 56);
                    b[r, c].Tag = k;
                    b[r, c].FlatStyle = FlatStyle.Flat; //design
                    b[r, c].FlatAppearance.BorderSize = 1;//design
                    if (z == 0)
                    {
                        z = 1;
                        b[r, c].BackColor = Color.FromArgb(240, 220, 130);
                    }
                    else
                    {
                        z = 0;
                        b[r, c].BackColor = Color.FromArgb(255, 253, 208);
                    }
                    b[r, c].BackgroundImageLayout = ImageLayout.Zoom;//design
                    b[r, c].Click += Window_Click;
                    Chess.Controls.Add(b[r, c]);
                    k++;
                }
                if (z == 1)
                { z = 0; }
                else
                { z = 1; }
            }
            #region Pawn
            a[1, 0] = 1;
            b[1, 0].BackgroundImage = w_pawn;
            a[1, 1] = 1;
            b[1, 1].BackgroundImage = w_pawn;
            a[1, 2] = 1;
            b[1, 2].BackgroundImage = w_pawn;
            a[1, 3] = 1;
            b[1, 3].BackgroundImage = w_pawn;
            a[1, 4] = 1;
            b[1, 4].BackgroundImage = w_pawn;
            a[1, 5] = 1;
            b[1, 5].BackgroundImage = w_pawn;
            a[1, 6] = 1;
            b[1, 6].BackgroundImage = w_pawn;
            a[1, 7] = 1;
            b[1, 7].BackgroundImage = w_pawn;
            a[6, 0] = -1;
            b[6, 0].BackgroundImage = b_pawn;
            a[6, 1] = -1;
            b[6, 1].BackgroundImage = b_pawn;
            a[6, 2] = -1;
            b[6, 2].BackgroundImage = b_pawn;
            a[6, 3] = -1;
            b[6, 3].BackgroundImage = b_pawn;
            a[6, 4] = -1;
            b[6, 4].BackgroundImage = b_pawn;
            a[6, 5] = -1;
            b[6, 5].BackgroundImage = b_pawn;
            a[6, 6] = -1;
            b[6, 6].BackgroundImage = b_pawn;
            a[6, 7] = -1;
            b[6, 7].BackgroundImage = b_pawn;
            #endregion
            #region rook
            a[0, 0] = 2;
            b[0, 0].BackgroundImage = w_rook;
            a[0, 7] = 2;
            b[0, 7].BackgroundImage = w_rook;
            a[7, 0] = -2;
            b[7, 0].BackgroundImage = b_rook;
            a[7, 7] = -2;
            b[7, 7].BackgroundImage = b_rook;
            #endregion
            #region knights
            a[0, 1] = 3;
            b[0, 1].BackgroundImage = w_knight;
            a[0, 6] = 3;
            b[0, 6].BackgroundImage = w_knight;
            a[7, 1] = -3;
            b[7, 1].BackgroundImage = b_knight;
            a[7, 6] = -3;
            b[7, 6].BackgroundImage = b_knight;
            #endregion
            #region bishop
            a[0, 2] = 4;
            b[0, 2].BackgroundImage = w_bishop;
            a[0, 5] = 4;
            b[0, 5].BackgroundImage = w_bishop;
            a[7, 2] = -4;
            b[7, 2].BackgroundImage = b_bishop;
            a[7, 5] = -4;
            b[7, 5].BackgroundImage = b_bishop;
            #endregion
            #region queen
            a[0, 3] = 5;
            b[0, 3].BackgroundImage = w_queen;
            a[7, 3] = -5;
            b[7, 3].BackgroundImage = b_queen;
            #endregion
            #region king
            a[0, 4] = 6;
            b[0, 4].BackgroundImage = w_king;
            a[7, 4] = -6;
            b[7, 4].BackgroundImage = b_king;
            #endregion
            k = 0;
        }

        private void Window_Click(object sender, EventArgs e)
        {
            string d = (((Button)(sender)).Tag).ToString();
            int n = int.Parse(d); //number of button
            c = n % 8;//colom
            r = n / 8;//row
            if (Turn_label.Text == "Turn: White")
            {
                if (k == 0 && a[r, c] > 0)
                {
                    if (a[r, c] == 1 && a[r + 1, c] == 0)//pawn
                    {
                        if (r == 1 && a[r + 2, c] == 0)//first move
                        {
                            b[r + 1, c].ForeColor = Color.Green;
                            b[r + 1, c].FlatAppearance.BorderSize = 3;
                            b[r + 2, c].ForeColor = Color.Green;
                            b[r + 2, c].FlatAppearance.BorderSize = 3;
                            k = 1;
                            r2 = r;
                            c2 = c;
                        }
                        else
                        {
                            b[r + 1, c].ForeColor = Color.Green;
                            b[r + 1, c].FlatAppearance.BorderSize = 3;
                            k = 1;
                            r2 = r;
                            c2 = c;
                        }
                    }
                }
                else if (k != 0 )
                {
                    if (r2==r&&c2==c)
                    {
                        Clean(b);
                        k = 0;
                    }
                    if (k == 1 && c2 == c)//pawn
                    {
                        if (r2==1 && r2 == r - 2 && a[r, c]==0)
                        {
                            a[r, c] = 1;
                            b[r, c].BackgroundImage = w_pawn;
                            a[r2, c2] = 0;
                            b[r2, c2].BackgroundImage = null;
                            Clean(b);
                            //Turn_label.Text = "Turn: Black";
                            k = 0;
                        }
                        else if (r2 == r - 1)
                        {
                            a[r, c] = 1;
                            b[r, c].BackgroundImage = w_pawn;
                            a[r2, c2] = 0;
                            b[r2, c2].BackgroundImage = null;
                            Clean(b);
                            //Turn_label.Text = "Turn: Black";
                            k = 0;
                        }
                    }
                }
            }
            else if (Turn_label.Text == "Turn: Black" && a[r, c] < 0)
            {
                //Turn_label.Text = "Turn: White";
            }
        }
    }
}
