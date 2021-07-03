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
        int[,] a1 = new int[8, 8];
        int[] rc = new int[2];
        int r, c, k, z, r2, c2, mov = 4, i, j;
        bool white_casteling_firstmove = false, black_casteling_firstmove = false;
        bool check1 = false;
        private readonly Image b_pawn = Properties.Resources.b_pawn;
        private readonly Image w_pawn = Properties.Resources.w_pawn;
        private readonly Image b_rook = Properties.Resources.b_rook;
        private readonly Image w_rook = Properties.Resources.w_rook;
        private readonly Image b_knight = Properties.Resources.b_knight;
        private readonly Image w_knight = Properties.Resources.w_knight;
        private readonly Image b_bishop = Properties.Resources.b_bishop;
        private readonly Image w_bishop = Properties.Resources.w_bishop;
        private readonly Image b_queen = Properties.Resources.b_queen;
        private readonly Image w_queen = Properties.Resources.w_queen;
        private readonly Image b_king = Properties.Resources.b_king;
        private readonly Image w_king = Properties.Resources.w_king;

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

        static void Show(int[,] a)
        {
            string msg = "";
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < a.GetLength(1); j++)
                {
                    msg += a[i, j].ToString() + "\t";
                }
                msg += "\n";
            }
            MessageBox.Show(msg);
        }

        static bool W_check(int[,] a)
        {
            int r, c, z, pp;
            for (r = 0; r < 8; r++)
            {
                for (c = 0; c < 8; c++)
                {
                    if (a[r, c] == -1)
                    {
                        if (r > 0 && c < 7)
                        {
                            if (a[r - 1, c + 1] == 6)
                                return true;
                        }
                        if (r > 0 && c > 0)
                        {
                            if (a[r - 1, c - 1] == 6)
                                return true;
                        }
                    } //pawn
                    else if (a[r, c] == -2)
                    {
                        if (r < 7)
                        {
                            if (a[r + 1, c] >= 0)
                            {
                                z = 1;
                                while (z < 8 - r)//down
                                {
                                    if (a[r + z, c] == 0) { }
                                    else if (a[r + z, c] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0)
                        {
                            if (a[r - 1, c] >= 0)
                            {
                                z = 1;
                                while (z < r+1)//up
                                {
                                    if (a[r - z, c] == 0) { }
                                    else if (a[r - z, c] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c < 7)
                        {
                            if (a[r, c + 1] >= 0)
                            {
                                z = 1;
                                while (z < 8 - c)//right
                                {
                                    if (a[r, c + z] == 0) { }
                                    else if (a[r, c + z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c > 0)
                        {
                            if (a[r, c - 1] >= 0)
                            {
                                z = 1;
                                while (z < c+1)//left
                                {
                                    if (a[r, c - z] == 0) { }
                                    else if (a[r, c - z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                    } //rook
                    else if (a[r, c] == -3)
                    {
                        if (r < 6 && c < 7)
                        {
                            if (a[r + 2, c + 1] == 6) //1
                                return true;
                        }
                        if (r < 6 && c > 0)
                        {
                            if (a[r + 2, c - 1] == 6) //2
                                return true;
                        }
                        if (r > 1 && c < 7)
                        {
                            if (a[r - 2, c + 1] == 6) //3
                                return true;
                        }
                        if (r > 1 && c > 0)
                        {
                            if (a[r - 2, c - 1] == 6) //4
                                return true;
                        }
                        if (r < 7 && c < 6)
                        {
                            if (a[r + 1, c + 2] == 6) //5
                                return true;
                        }
                        if (r > 0 && c < 6)
                        {
                            if (a[r - 1, c + 2] == 6) //6
                                return true;
                        }
                        if (r > 0 && c > 1)
                        {
                            if (a[r - 1, c - 2] == 6) //7
                                return true;
                        }
                        if (r < 7 && c > 1)
                        {
                            if (a[r + 1, c - 2] == 6)//8
                                return true;
                        }
                    } //knight
                    else if (a[r, c] == -4)//bishop
                    {
                        if (r < 7 && c < 7)
                        {
                            if (a[r + 1, c + 1] >= 0)
                            {
                                if (r > c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < 8 - pp)//down right
                                {
                                    if (a[r + z, c + z] == 0) { }
                                    else if (a[r + z, c + z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c > 0)
                        {
                            if (a[r - 1, c - 1] >= 0)
                            {
                                if (r < c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < pp+1)//up left
                                {
                                    if (a[r - z, c - z] == 0) { }
                                    else if (a[r - z, c - z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c < 7)
                        {
                            if (a[r - 1, c + 1] >= 0)
                            {
                                if (r < 8 - c)
                                    pp = r+1;
                                else
                                    pp = 8 - c;
                                z = 1;
                                while (z < pp)//up right
                                {
                                    if (a[r - z, c + z] == 0) { }
                                    else if (a[r - z, c + z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r < 7 && c > 0)
                        {
                            if (a[r + 1, c - 1] >= 0)
                            {
                                if (c < 8 - r)
                                    pp = c+1;
                                else
                                    pp = 8 - r;
                                z = 1;
                                while (z < pp)//down left
                                {
                                    if (a[r + z, c - z] == 0) { }
                                    else if (a[r + z, c - z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                    } //bishop
                    else if (a[r, c] == -5)
                    {
                        if (r < 7)
                        {
                            if (a[r + 1, c] >= 0)
                            {
                                z = 1;
                                while (z < 8-r)//down
                                {
                                    if (a[r + z, c] == 0) { }
                                    else if (a[r + z, c] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0)
                        {
                            if (a[r - 1, c] >= 0)
                            {
                                z = 1;
                                while (z < r+1)//up
                                {
                                    if (a[r - z, c] == 0) { }
                                    else if (a[r - z, c] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c < 7)
                        {
                            if (a[r, c + 1] >= 0)
                            {
                                z = 1;
                                while (z < 8-c)//right
                                {
                                    if (a[r, c + z] == 0) { }
                                    else if (a[r, c + z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c > 0)
                        {
                            if (a[r, c - 1] >= 0)
                            {
                                z = 1;
                                while (z < c+1)//left
                                {
                                    if (a[r, c - z] == 0) { }
                                    else if (a[r, c - z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r < 7 && c < 7)
                        {
                            if (a[r + 1, c + 1] >= 0)
                            {
                                if (r > c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < 8 - pp)//down right
                                {
                                    if (a[r + z, c + z] == 0) { }
                                    else if (a[r + z, c + z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c > 0)
                        {
                            if (a[r - 1, c - 1] >= 0)
                            {
                                if (r < c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < pp+1)//up left
                                {
                                    if (a[r - z, c - z] == 0) { }
                                    else if (a[r - z, c - z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c < 7)
                        {
                            if (a[r - 1, c + 1] >= 0)
                            {
                                if (r < 8 - c)
                                    pp = r+1;
                                else
                                    pp = 8 - c;
                                z = 1;
                                while (z < pp)//up right
                                {
                                    if (a[r - z, c + z] == 0) { }
                                    else if (a[r - z, c + z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r < 7 && c > 0)
                        {
                            if (a[r + 1, c - 1] >= 0)
                            {
                                if (c < 8 - r)
                                    pp = c+1;
                                else
                                    pp = 8 - r;
                                z = 1;
                                while (z < pp)//down left
                                {
                                    if (a[r + z, c - z] == 0) { }
                                    else if (a[r + z, c - z] == 6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                    } //queen
                    else if (a[r, c] == -6)
                    {
                        if (r > 0)
                        {
                            if (a[r - 1, c] == 6)//up
                                return true;
                        }
                        if (r < 7)
                        {
                            if (a[r + 1, c] == 6)//down
                                return true;
                        }
                        if (c > 0)
                        {
                            if (a[r, c - 1] == 6)//left
                                return true;
                        }
                        if (c < 7)
                        {
                            if (a[r, c + 1] == 6)//right
                                return true;
                        }
                        if (r > 0 && c > 0)
                        {
                            if (a[r - 1, c - 1] == 6)//up left
                                return true;
                        }
                        if (r > 0 && c < 7)
                        {
                            if (a[r - 1, c + 1] == 6)//up right
                                return true;
                        }
                        if (r < 7 && c > 0)
                        {
                            if (a[r + 1, c - 1] == 6)//down left
                                return true;
                        }
                        if (r < 7 && c < 7)
                        {
                            if (a[r + 1, c + 1] == 6)//down right
                                return true;
                        }
                    } //king
                }
            }
            return false;
        }
        static bool B_check(int[,] a)
        {
            int r, c, z, pp;
            for (r = 0; r < 8; r++)
            {
                for (c = 0; c < 8; c++)
                {
                    if (a[r, c] == 1)
                    {
                        if (r < 7 && c < 7)
                        {
                            if (a[r + 1, c + 1] == -6)
                                return true;
                        }
                        if (r < 7 && c > 0)
                        {
                            if (a[r + 1, c - 1] == -6)
                                return true;
                        }
                    } //pawn
                    else if (a[r, c] == 2)
                    {
                        if (r < 7)
                        {
                            if (a[r + 1, c] <= 0)
                            {
                                z = 1;
                                while (z < 8-r)//down
                                {
                                    if (a[r + z, c] == 0) { }
                                    else if (a[r + z, c] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0)
                        {
                            if (a[r - 1, c] <= 0)
                            {
                                z = 1;
                                while (z < r+1)//up
                                {
                                    if (a[r - z, c] == 0) { }
                                    else if (a[r - z, c] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c < 7)
                        {
                            if (a[r, c + 1] <= 0)
                            {
                                z = 1;
                                while (z < 8-c)//right
                                {
                                    if (a[r, c + z] == 0) { }
                                    else if (a[r, c + z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c > 0)
                        {
                            if (a[r, c - 1] <= 0)
                            {
                                z = 1;
                                while (z < c+1)//left
                                {
                                    if (a[r, c - z] == 0) { }
                                    else if (a[r, c - z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                    } //rook
                    else if (a[r, c] == 3)
                    {
                        if (r < 6 && c < 7)
                        {
                            if (a[r + 2, c + 1] == -6) //1
                                return true;
                        }
                        if (r < 6 && c > 0)
                        {
                            if (a[r + 2, c - 1] == -6) //2
                                return true;
                        }
                        if (r > 1 && c < 7)
                        {
                            if (a[r - 2, c + 1] == -6) //3
                                return true;
                        }
                        if (r > 1 && c > 0)
                        {
                            if (a[r - 2, c - 1] == -6) //4
                                return true;
                        }
                        if (r < 7 && c < 6)
                        {
                            if (a[r + 1, c + 2] == -6) //5
                                return true;
                        }
                        if (r > 0 && c < 6)
                        {
                            if (a[r - 1, c + 2] == -6) //6
                                return true;
                        }
                        if (r > 0 && c > 1)
                        {
                            if (a[r - 1, c - 2] == -6) //7
                                return true;
                        }
                        if (r < 7 && c > 1)
                        {
                            if (a[r + 1, c - 2] == -6)//8
                                return true;
                        }
                    } //knight
                    else if (a[r, c] == 4)//bishop
                    {
                        if (r < 7 && c < 7)
                        {
                            if (a[r + 1, c + 1] <= 0)
                            {
                                if (r > c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < 8 - pp)//down right
                                {
                                    if (a[r + z, c + z] == 0) { }
                                    else if (a[r + z, c + z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c > 0)
                        {
                            if (a[r - 1, c - 1] <= 0)
                            {
                                if (r < c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < pp+1)//up left
                                {
                                    if (a[r - z, c - z] == 0) { }
                                    else if (a[r - z, c - z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c < 7)
                        {
                            if (a[r - 1, c + 1] <= 0)
                            {
                                if (r < 8 - c)
                                    pp = r+1;
                                else
                                    pp = 8 - c;
                                z = 1;
                                while (z < pp)//up right
                                {
                                    if (a[r - z, c + z] == 0) { }
                                    else if (a[r - z, c + z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r < 7 && c > 0)
                        {
                            if (a[r + 1, c - 1] <= 0)
                            {
                                if (c < 8 - r)
                                    pp = c+1;
                                else
                                    pp = 8 - r;
                                z = 1;
                                while (z < pp)//down left
                                {
                                    if (a[r + z, c - z] == 0) { }
                                    else if (a[r + z, c - z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                    } //bishop
                    else if (a[r, c] == 5)
                    {
                        if (r < 7)
                        {
                            if (a[r + 1, c] <= 0)
                            {
                                z = 1;
                                while (z < 8-r)//down
                                {
                                    if (a[r + z, c] == 0) { }
                                    else if (a[r + z, c] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0)
                        {
                            if (a[r - 1, c] <= 0)
                            {
                                z = 1;
                                while (z < r+1)//up
                                {
                                    if (a[r - z, c] == 0) { }
                                    else if (a[r - z, c] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c < 7)
                        {
                            if (a[r, c + 1] <= 0)
                            {
                                z = 1;
                                while (z < 8-c)//right
                                {
                                    if (a[r, c + z] == 0) { }
                                    else if (a[r, c + z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (c > 0)
                        {
                            if (a[r, c - 1] <= 0)
                            {
                                z = 1;
                                while (z < c+1)//left
                                {
                                    if (a[r, c - z] == 0) { }
                                    else if (a[r, c - z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r < 7 && c < 7)
                        {
                            if (a[r + 1, c + 1] <= 0)
                            {
                                if (r > c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < 8-pp)//down right
                                {
                                    if (a[r + z, c + z] == 0) { }
                                    else if (a[r + z, c + z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c > 0)
                        {
                            if (a[r - 1, c - 1] <= 0)
                            {
                                if (r < c)
                                    pp = r;
                                else
                                    pp = c;
                                z = 1;
                                while (z < pp+1)//up left
                                {
                                    if (a[r - z, c - z] == 0) { }
                                    else if (a[r - z, c - z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r > 0 && c < 7)
                        {
                            if (a[r - 1, c + 1] <= 0)
                            {
                                if (r < 8 - c)
                                    pp = r+1;
                                else
                                    pp = 8 - c;
                                z = 1;
                                while (z < pp)//up right
                                {
                                    if (a[r - z, c + z] == 0) { }
                                    else if (a[r - z, c + z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                        if (r < 7 && c > 0)
                        {
                            if (a[r + 1, c - 1] <= 0)
                            {
                                if (c < 8 - r)
                                    pp = c+1;
                                else
                                    pp = 8 - r;
                                z = 1;
                                while (z < pp)//down left
                                {
                                    if (a[r + z, c - z] == 0) { }
                                    else if (a[r + z, c - z] == -6)
                                        return true;
                                    else
                                        z = 8;  //break
                                    z++;
                                }
                            }
                        }
                    } //queen
                    else if (a[r, c] == 6)
                    {
                        if (r > 0)
                        {
                            if (a[r - 1, c] == -6)//up
                                return true;
                        }
                        if (r < 7)
                        {
                            if (a[r + 1, c] == -6)//down
                                return true;
                        }
                        if (c > 0)
                        {
                            if (a[r, c - 1] == -6)//left
                                return true;
                        }
                        if (c < 7)
                        {
                            if (a[r, c + 1] == -6)//right
                                return true;
                        }
                        if (r > 0 && c > 0)
                        {
                            if (a[r - 1, c - 1] == -6)//up left
                                return true;
                        }
                        if (r > 0 && c < 7)
                        {
                            if (a[r - 1, c + 1] == -6)//up right
                                return true;
                        }
                        if (r < 7 && c > 0)
                        {
                            if (a[r + 1, c - 1] == -6)//down left
                                return true;
                        }
                        if (r < 7 && c < 7)
                        {
                            if (a[r + 1, c + 1] == -6)//down right
                                return true;
                        }
                    } //king
                }
            }
            return false;
        }

        static void W_checkmate(int[,] a)
        {
            int r, c;
            int[] rc = new int[4];
            rc=Comp_W(a, 1);
            r = rc[0];
            c = rc[1];
            if (a[r, c] == 0)
            {
                MessageBox.Show("stalemate");
                Application.Restart();
            }
        }
        static void B_checkmate(int[,] a)
        {
            int r, c;
            int[] rc = new int[4];
            rc=Comp_B(a, 1);
            r = rc[0];
            c = rc[1];
            if (a[r, c] == 0)
            {
                MessageBox.Show("stalemate");
                Application.Restart();
            }
        }

        static int Score_W(int[,] a)
        {
            int score = 0, r, c;
            for (r = 0; r < a.GetLength(0); r++)
                for (c = 0; c < a.GetLength(0); c++)
                {
                    if (a[r, c] > 0)
                    {
                        if (a[r, c] == 1)
                        {
                            score += 1;
                        }
                        else if (a[r, c] == 2)
                        {
                            score += 8;
                        }
                        else if (a[r, c] == 3)
                        {
                            score += 24;
                        }
                        else if (a[r, c] == 4)
                        {
                            score += 64;
                        }
                        else if (a[r, c] == 5)
                        {
                            score += 128;
                        }
                    }
                    else if (a[r, c] < 0)
                    {
                        if (a[r, c] == -1)
                        {
                            score -= 1;
                        }
                        else if (a[r, c] == -2)
                        {
                            score -= 8;
                        }
                        else if (a[r, c] == -3)
                        {
                            score -= 24;
                        }
                        else if (a[r, c] == -4)
                        {
                            score -= 64;
                        }
                        else if (a[r, c] == -5)
                        {
                            score -= 128;
                        }
                    }
                }
            return score;
        }
        static int Score_B(int[,] a)
        {
            int score = 0, r, c;
            for (r = 0; r < a.GetLength(0); r++)
                for (c = 0; c < a.GetLength(0); c++)
                {
                    if (a[r, c] < 0)
                    {
                        if (a[r, c] == -1)
                        {
                            score += 1;
                        }
                        else if (a[r, c] == -2)
                        {
                            score += 8;
                        }
                        else if (a[r, c] == -3)
                        {
                            score += 24;
                        }
                        else if (a[r, c] == -4)
                        {
                            score += 64;
                        }
                        else if (a[r, c] == -5)
                        {
                            score += 128;
                        }
                    }
                    if (a[r, c] > 0)
                    {
                        if (a[r, c] == 1)
                        {
                            score -= 1;
                        }
                        else if (a[r, c] == 2)
                        {
                            score -= 8;
                        }
                        else if (a[r, c] == 3)
                        {
                            score -= 24;
                        }
                        else if (a[r, c] == 4)
                        {
                            score -= 64;
                        }
                        else if (a[r, c] == 5)
                        {
                            score -= 128;
                        }
                    }
                }
            return score;
        }

        static int[] Comp_W(int[,] a, int mov)
        {
            int score1 = 0, score, r = -1, c = -1, r2, c2, z, pp;
            if (mov % 2 == 0)
                score = 10000;
            else
                score = -10000;
            Boolean check = false;
            int[] rc = new int[4];
            int[,] a1 = new int[a.GetLength(0), a.GetLength(0)];
            for (r2 = 0; r2 < a.GetLength(0); r2++)
            {
                for (c2 = 0; c2 < a.GetLength(1); c2++)
                {
                    a1[r2, c2] = a[r2, c2];
                }
            }
            for (r = 0; r < a.GetLength(0); r++)
            {
                for (c = 0; c < a.GetLength(1); c++)
                {
                    if (a[r, c] > 0)
                    {
                        if (a[r, c] == 1)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] == 0)
                                {
                                        if (r == 1 && a[r + 2, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + 2, c] = 1;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + 2;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + 2;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                    a1[r, c] = 0;
                                    a1[r + 1, c] = 1;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //straight
                            }
                            if(r<7&&c<7)
                            {
                                if (a[r + 1, c + 1] < 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 1] = 1;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }

                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat right
                            }
                            if(r<7&&c>0)
                            {
                                if (a[r + 1, c - 1] < 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 1] = 1;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat left
                            }
                        } //pawn
                        else if (a[r, c] == 2)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(r>0)
                            {
                                if (a[r - 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c<7)
                            {
                                if (a[r, c + 1] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c>0)
                            {
                                if (a[r, c - 1] <= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //rook
                        else if (a[r, c] == 3)
                        {
                            if(r<6&&c<7)
                            {
                                if (a[r + 2, c + 1] <= 0) //1
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c + 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 6 && c >0)
                            {
                                if (a[r + 2, c - 1] <= 0) //2
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c - 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r >1 && c < 7)
                            {
                                if (a[r - 2, c + 1] <= 0) //3
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c + 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r >1 && c >0)
                            {
                                if (a[r - 2, c - 1] <= 0) //4
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c - 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c < 6)
                            {
                                if (a[r + 1, c + 2] <= 0) //5
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c < 6)
                            {
                                if (a[r - 1, c + 2] <= 0) //6
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r >0 && c >1)
                            {
                                if (a[r - 1, c - 2] <= 0) //7
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c > 1)
                            {
                                if (a[r + 1, c - 2] <= 0)//8
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //knight
                        else if (a[r, c] == 4)//bishop
                        {
                            if(r<7&&c<7)
                            {
                                if (a[r + 1, c + 1] <= 0)
                                {
                                    if (r > c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8-pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r >0 && c >0)
                            {
                                if (a[r - 1, c - 1] <= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp+1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c <7)
                            {
                                if (a[r - 1, c + 1] < 0)
                                {
                                    if (r < 8 - c)
                                        pp = r + 1;
                                    else
                                        pp = 8 - c;
                                    z = 1;
                                    while (z < pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r <7 && c >0)
                            {
                                if (a[r + 1, c - 1] <= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c + 1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //bishop
                        else if (a[r, c] == 5)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(r>0)
                            {
                                if (a[r - 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c<7)
                            {
                                if (a[r, c + 1] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c>0)
                            {
                                if (a[r, c - 1] <= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }

                            }
                            if(r<7&&c<7)
                            {
                                if (a[r + 1, c + 1] <= 0)
                                {
                                    if (r > c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8 - pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(r>0&&c>0)
                            {
                                if (a[r - 1, c - 1] <= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp + 1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] <= 0)
                                {
                                    if (r < 8 - c)
                                        pp = r + 1;
                                    else
                                        pp = 8 - c;
                                    z = 1;
                                    while (z < pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] <= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c + 1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 5;
                                            if (mov == 1)
                                                score1 = Score_W(a1);
                                            else
                                            {
                                                score1 = Comp_b_scr(a1, mov - 1, score);
                                            }
                                            if (mov % 2 == 0)
                                            {
                                                if (score1 < score)
                                                {
                                                    rc[0] = r;
                                                    rc[1] = c;
                                                    rc[2] = r + z;
                                                    rc[3] = c - z;
                                                    score = score1;
                                                }
                                            }
                                            else
                                            {
                                                if (score1 > score)
                                                {
                                                    rc[0] = r;
                                                    rc[1] = c;
                                                    rc[2] = r + z;
                                                    rc[3] = c - z;
                                                    score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 5;
                                            if (mov == 1)
                                                score1 = Score_W(a1);
                                            else
                                            {
                                                score1 = Comp_b_scr(a1, mov - 1, score);
                                            }
                                            if (mov % 2 == 0)
                                            {
                                                if (score1 < score)
                                                {
                                                    rc[0] = r;
                                                    rc[1] = c;
                                                    rc[2] = r + z;
                                                    rc[3] = c - z;
                                                    score = score1;
                                                }
                                            }
                                            else
                                            {
                                                if (score1 > score)
                                                {
                                                    rc[0] = r;
                                                    rc[1] = c;
                                                    rc[2] = r + z;
                                                    rc[3] = c - z;
                                                    score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //queen
                        else if (a[r, c] == 6)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] <= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r>0)
                            {
                                if (a[r - 1, c] <= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r<7&&c<7)
                            {
                                if (a[r + 1, c + 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r<7&&c>0)
                            {
                                if (a[r + 1, c - 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r>0&&c<7)
                            {
                                if (a[r - 1, c + 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r>0&&c>0)
                            {
                                if (a[r - 1, c - 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(c<7)
                            {
                                if (a[r, c + 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c + 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(c>0)
                            {
                                if (a[r, c - 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c - 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //king
                    }
                }
            }
            if (rc[0]==rc[2]&&rc[1]==rc[3]&&mov==1)
            {
                if (W_check(a)==true)
                    MessageBox.Show("checkmate");
                else
                    MessageBox.Show("stalemate");
                Application.Restart();
            }
            return rc;
        }
        static int[] Comp_B(int[,] a, int mov)
        {
            int score1 = 0, score, r = -1, c = -1, r2, c2, z, pp;
            if (mov % 2 == 0)
                score = 10000;
            else
                score = -10000;
            Boolean check = false;
            int[] rc = new int[4];
            int[,] a1 = new int[a.GetLength(0), a.GetLength(0)];
            for (r2 = 0; r2 < a.GetLength(0); r2++)
            {
                for (c2 = 0; c2 < a.GetLength(1); c2++)
                {
                    a1[r2, c2] = a[r2, c2];
                }
            }
            for (r = 0; r < a.GetLength(0); r++)
            {
                for (c = 0; c < a.GetLength(1); c++)
                {
                    if (a[r, c] < 0)
                    {
                        if (a[r, c] == -1)
                        {
                            if (r >0)
                            {
                                if (a[r - 1, c] == 0)
                                {
                                    if (r == 6 && a[r - 2, c] == 0)
                                    {
                                        a1[r, c] = 0;
                                        a1[r - 2, c] = -1;
                                        check = B_check(a1);
                                        if (check == false)
                                        {
                                            if (mov == 1)
                                                score1 = Score_B(a1);
                                            else
                                            {
                                                score1 = Comp_w_scr(a1, mov - 1, score);
                                            }
                                            if (mov % 2 == 0)
                                            {
                                                if (score1 < score)
                                                {
                                                    rc[0] = r;
                                                    rc[1] = c;
                                                    rc[2] = r - 2;
                                                    rc[3] = c;
                                                    score = score1;
                                                }
                                            }
                                            else
                                            {
                                                if (score1 > score)
                                                {
                                                    rc[0] = r;
                                                    rc[1] = c;
                                                    rc[2] = r - 2;
                                                    rc[3] = c;
                                                    score = score1;
                                                }
                                            }
                                        }
                                        for (r2 = 0; r2 < a.GetLength(0); r2++)
                                        {
                                            for (c2 = 0; c2 < a.GetLength(1); c2++)
                                            {
                                                a1[r2, c2] = a[r2, c2];
                                            }
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c] = -1;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //straight
                            }
                            if (r >0 && c < 7)
                            {
                                if (a[r - 1, c + 1] > 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 1] = -1;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat right
                            }
                            if (r >0&& c > 0)
                            {
                                if (a[r - 1, c - 1] > 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 1] = -1;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat left
                            }
                        } //pawn
                        else if (a[r, c] == -2)
                        {
                            if (r < 7)
                            {
                                if (a[r + 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0)
                            {
                                if (a[r - 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c < 7)
                            {
                                if (a[r, c + 1] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c > 0)
                            {
                                if (a[r, c - 1] >= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //rook
                        else if (a[r, c] == -3)
                        {
                            if (r < 6 && c < 7)
                            {
                                if (a[r + 2, c + 1] >= 0) //1
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c + 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 6 && c > 0)
                            {
                                if (a[r + 2, c - 1] >= 0) //2
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c - 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 1 && c < 7)
                            {
                                if (a[r - 2, c + 1] >= 0) //3
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c + 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 1 && c > 0)
                            {
                                if (a[r - 2, c - 1] >= 0) //4
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c - 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 2;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c < 6)
                            {
                                if (a[r + 1, c + 2] >= 0) //5
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c < 6)
                            {
                                if (a[r - 1, c + 2] >= 0) //6
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c > 1)
                            {
                                if (a[r - 1, c - 2] >= 0) //7
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c > 1)
                            {
                                if (a[r + 1, c - 2] >= 0)//8
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 2;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //knight
                        else if (a[r, c] == -4)//bishop
                        {
                            if (r < 7 && c < 7)
                            {
                                if (a[r + 1, c + 1] >= 0)
                                {
                                    if (r > c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8 - pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c > 0)
                            {
                                if (a[r - 1, c - 1] >= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp + 1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] >= 0)
                                {
                                    if (r < 8 - c)
                                        pp = r + 1;
                                    else
                                        pp = 8 - c;
                                    z = 1;
                                    while (z < pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] >= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c + 1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //bishop
                        else if (a[r, c] == -5)
                        {
                            if (r < 7)
                            {
                                if (a[r + 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0)
                            {
                                if (a[r - 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c < 7)
                            {
                                if (a[r, c + 1] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c > 0)
                            {
                                if (a[r, c - 1] >= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }

                            }
                            if (r < 7 && c < 7)
                            {
                                if (a[r + 1, c + 1] >= 0)
                                {
                                    if (r > c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8 - pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c > 0)
                            {
                                if (a[r - 1, c - 1] >= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp + 1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] >= 0)
                                {
                                    if (r < 8 - c)
                                        pp = r + 1;
                                    else
                                        pp = 8 - c;
                                    z = 1;
                                    while (z < pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r - z;
                                                        rc[3] = c + z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] >= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c + 1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                                else
                                                {
                                                    if (score1 > score)
                                                    {
                                                        rc[0] = r;
                                                        rc[1] = c;
                                                        rc[2] = r + z;
                                                        rc[3] = c - z;
                                                        score = score1;
                                                    }
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //queen
                        else if (a[r, c] == -6)
                        {
                            if (r < 7)
                            {
                                if (a[r + 1, c] >= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0)
                            {
                                if (a[r - 1, c] >= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c < 7)
                            {
                                if (a[r + 1, c + 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r + 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c > 0)
                            {
                                if (a[r - 1, c - 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r - 1;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (c < 7)
                            {
                                if (a[r, c + 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c + 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c + 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (c > 0)
                            {
                                if (a[r, c - 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c - 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                        else
                                        {
                                            if (score1 > score)
                                            {
                                                rc[0] = r;
                                                rc[1] = c;
                                                rc[2] = r;
                                                rc[3] = c - 1;
                                                score = score1;
                                            }
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //king 
                    }
                }
            }
            if (rc[0] == rc[2] && rc[1] == rc[3] && mov == 1)
            {
                if (B_check(a) == true)
                    MessageBox.Show("checkmate");
                else
                    MessageBox.Show("stalemate");
                Application.Restart();
            }
            return rc;
        }

        static int Comp_w_scr(int[,] a, int mov, int lastscore)
        {
            int score, score1, r, c, r2, c2, z, pp;
            if (mov % 2 == 0)
            {
                score = 10000;
                if (mov == 1)
                    score = 2000;
                if (lastscore > 0)
                    lastscore = -lastscore;
            }
            else
            {
                score = -10000;
                if (mov == 1)
                    score = -2000;
                if (lastscore < 0)
                    lastscore = -lastscore;
            }
            Boolean check = false;
            int[,] a1 = new int[a.GetLength(0), a.GetLength(0)];
            for (r2 = 0; r2 < a.GetLength(0); r2++)
            {
                for (c2 = 0; c2 < a.GetLength(1); c2++)
                {
                    a1[r2, c2] = a[r2, c2];
                }
            }
            for (r = 0; r < a.GetLength(0); r++)
            {
                for (c = 0; c < a.GetLength(1); c++)
                {
                    if (a[r, c] > 0)
                    {
                        if (a[r, c] == 1)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] == 0)
                                {
                                    if (r == 1 && a[r + 2, c] == 0)
                                    {
                                        a1[r, c] = 0;
                                        a1[r + 2, c] = 1;
                                        check = W_check(a1);
                                        if (check == false)
                                        {
                                            if (mov == 1)
                                                score1 = Score_W(a1);
                                            else
                                            {
                                                score1 = Comp_b_scr(a1, mov - 1, score);
                                            }
                                            if (mov % 2 == 0)
                                            {
                                                if (score1 < lastscore)
                                                    return score1;
                                                if (score1 < score)
                                                    score = score1;
                                            }
                                            else
                                            {
                                                if (score1 > lastscore)
                                                    return score1;
                                                if (score1 > score)
                                                    score = score1;
                                            }
                                        }
                                        for (r2 = 0; r2 < a.GetLength(0); r2++)
                                        {
                                            for (c2 = 0; c2 < a.GetLength(1); c2++)
                                            {
                                                a1[r2, c2] = a[r2, c2];
                                            }
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c] = 1;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //straight
                            }
                            if(r<7&&c<7)
                            {
                                if (a[r + 1, c + 1] < 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 1] = 1;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat right
                            }
                            if(r<7&&c>0)
                            {
                                if (a[r + 1, c - 1] < 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 1] = 1;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat left
                            }
                        } //pawn
                        else if (a[r, c] == 2)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(r>0)
                            {
                                if (a[r - 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c<7)
                            {
                                if (a[r, c + 1] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c>0)
                            {
                                if (a[r, c - 1] <= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 2;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //rook
                        else if (a[r, c] == 3)
                        {
                            if (r < 6 && c < 7)
                            {
                                if (a[r + 2, c + 1] <= 0) //1
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c + 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 6 && c > 0)
                            {
                                if (a[r + 2, c - 1] <= 0) //2
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c - 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 1 && c < 7)
                            {
                                if (a[r - 2, c + 1] <= 0) //3
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c + 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 1 && c > 0)
                            {
                                if (a[r - 2, c - 1] <= 0) //4
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c - 1] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c < 6)
                            {
                                if (a[r + 1, c + 2] <= 0) //5
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c < 6)
                            {
                                if (a[r - 1, c + 2] <= 0) //6
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c > 1)
                            {
                                if (a[r - 1, c - 2] <= 0) //7
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c > 1)
                            {
                                if (a[r + 1, c - 2] <= 0)//8
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 2] = 3;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //knight
                        else if (a[r, c] == 4)//bishop
                        {
                            if (r < 7 && c < 7)
                            {
                                if (a[r + 1, c + 1] <= 0)
                                {
                                    if (r > c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8-pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r >0 && c >0)
                            {
                                if (a[r - 1, c - 1] <= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp+1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c <7)
                            {
                                if (a[r - 1, c + 1] <= 0)
                                {
                                    if (r < 8-c)
                                        pp = r+1;
                                    else
                                        pp = 8-c;
                                    z = 1;
                                    while (z <pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r <7 && c >0)
                            {
                                if (a[r + 1, c - 1] <= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c+1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 4;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //bishop
                        else if (a[r, c] == 5)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(r>0)
                            {
                                if (a[r - 1, c] <= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c<7)
                            {
                                if (a[r, c + 1] <= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(c>0)
                            {
                                if (a[r, c - 1] <= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }

                            }
                            if(r<7&&c<7)
                            {
                                if (a[r + 1, c + 1] <= 0)
                                {
                                    if (r>c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8-pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if(r>0&&c>0)
                            {
                                if (a[r - 1, c - 1] <= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp+1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] <= 0)
                                {
                                    if (r < 8 - c)
                                        pp = r+1;
                                    else
                                        pp = 8 - c;
                                    z = 1;
                                    while (z < pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = 5;
                                            check = W_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_W(a1);
                                                else
                                                {
                                                    score1 = Comp_b_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] <= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c+1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 5;
                                            if (mov == 1)
                                                score1 = Score_W(a1);
                                            else
                                            {
                                                score1 = Comp_b_scr(a1, mov - 1, score);
                                            }
                                            if (mov % 2 == 0)
                                            {
                                                if (score1 < lastscore)
                                                    return score1;
                                                if (score1 < score)
                                                    score = score1;
                                            }
                                            else
                                            {
                                                if (score1 > lastscore)
                                                    return score1;
                                                if (score1 > score)
                                                    score = score1;
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] < 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = 5;
                                            if (mov == 1)
                                                score1 = Score_W(a1);
                                            else
                                            {
                                                score1 = Comp_b_scr(a1, mov - 1, score);
                                            }
                                            if (mov % 2 == 0)
                                            {
                                                if (score1 < lastscore)
                                                    return score1;
                                                if (score1 < score)
                                                    score = score1;
                                            }
                                            else
                                            {
                                                if (score1 > lastscore)
                                                    return score1;
                                                if (score1 > score)
                                                    score = score1;
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //queen
                        else if (a[r, c] == 6)
                        {
                            if(r<7)
                            {
                                if (a[r + 1, c] <= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r>0)
                            {
                                if (a[r - 1, c] <= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r<7&&c<7)
                            {
                                if (a[r + 1, c + 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r<7&&c>0)
                            {
                                if (a[r + 1, c - 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r>0&&c<7)
                            {
                                if (a[r - 1, c + 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(r>0&&c>0)
                            {
                                if (a[r - 1, c - 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(c<7)
                            {
                                if (a[r, c + 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c + 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if(c>0)
                            {
                                if (a[r, c - 1] <= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c - 1] = 6;
                                    check = W_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_W(a1);
                                        else
                                        {
                                            score1 = Comp_b_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //king
                    }
                }
            }
            return score;
        }
        static int Comp_b_scr(int[,] a, int mov, int lastscore)
        {
            int score, score1, r, c, r2, c2, z, pp;
            if (mov % 2 == 0)
            {
                score = 10000;
                if (mov == 1)
                    score = 2000;
                if (lastscore > 0)
                    lastscore = -lastscore;
            }
            else
            {
                score = -10000;
                if (mov == 1)
                    score = -2000;
                if (lastscore < 0)
                    lastscore = -lastscore;
            }
            Boolean check = false;
            int[,] a1 = new int[a.GetLength(0), a.GetLength(0)];
            for (r2 = 0; r2 < a.GetLength(0); r2++)
            {
                for (c2 = 0; c2 < a.GetLength(1); c2++)
                {
                    a1[r2, c2] = a[r2, c2];
                }
            }
            for (r = 0; r < a.GetLength(0); r++)
            {
                for (c = 0; c < a.GetLength(1); c++)
                {
                    if (a[r, c] < 0)
                    {
                        if (a[r, c] == -1)
                        {
                            if (r > 0)
                            {
                                if (a[r - 1, c] == 0)
                                {
                                    if (r == 6 && a[r - 2, c] == 0)
                                    {
                                        a1[r, c] = 0;
                                        a1[r - 2, c] = -1;
                                        check = B_check(a1);
                                        if (check == false)
                                        {
                                            if (mov == 1)
                                                score1 = Score_B(a1);
                                            else
                                            {
                                                score1 = Comp_w_scr(a1, mov - 1, score);
                                            }
                                            if (mov % 2 == 0)
                                            {
                                                if (score1 < lastscore)
                                                    return score1;
                                                if (score1 < score)
                                                    score = score1;
                                            }
                                            else
                                            {
                                                if (score1 > lastscore)
                                                    return score1;
                                                if (score1 > score)
                                                    score = score1;
                                            }
                                        }
                                        for (r2 = 0; r2 < a.GetLength(0); r2++)
                                        {
                                            for (c2 = 0; c2 < a.GetLength(1); c2++)
                                            {
                                                a1[r2, c2] = a[r2, c2];
                                            }
                                        }
                                    }
                                }
                                a1[r, c] = 0;
                                a1[r - 1, c] = -1;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    if (mov == 1)
                                        score1 = Score_B(a1);
                                    else
                                    {
                                        score1 = Comp_w_scr(a1, mov - 1, score);
                                    }
                                    if (mov % 2 == 0)
                                    {
                                        if (score1 < lastscore)
                                            return score1;
                                        if (score1 < score)
                                            score = score1;
                                    }
                                    else
                                    {
                                        if (score1 > lastscore)
                                            return score1;
                                        if (score1 > score)
                                            score = score1;
                                    }
                                }
                                for (r2 = 0; r2 < a.GetLength(0); r2++)
                                {
                                    for (c2 = 0; c2 < a.GetLength(1); c2++)
                                    {
                                        a1[r2, c2] = a[r2, c2];
                                    }
                                }
                            } //straight
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] > 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 1] = -1;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat right
                            }
                            if (r > 0 && c > 0)
                            {
                                if (a[r - 1, c - 1] > 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 1] = -1;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                } //eat left
                            }
                        } //pawn
                        else if (a[r, c] == -2)
                        {
                            if (r < 7)
                            {
                                if (a[r + 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0)
                            {
                                if (a[r - 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c < 7)
                            {
                                if (a[r, c + 1] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c > 0)
                            {
                                if (a[r, c - 1] >= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -2;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //rook
                        else if (a[r, c] == -3)
                        {
                            if (r < 6 && c < 7)
                            {
                                if (a[r + 2, c + 1] >= 0) //1
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c + 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 6 && c > 0)
                            {
                                if (a[r + 2, c - 1] >= 0) //2
                                {
                                    a1[r, c] = 0;
                                    a1[r + 2, c - 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 1 && c < 7)
                            {
                                if (a[r - 2, c + 1] >= 0) //3
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c + 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 1 && c > 0)
                            {
                                if (a[r - 2, c - 1] >= 0) //4
                                {
                                    a1[r, c] = 0;
                                    a1[r - 2, c - 1] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c < 6)
                            {
                                if (a[r + 1, c + 2] >= 0) //5
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c < 6)
                            {
                                if (a[r - 1, c + 2] >= 0) //6
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c > 1)
                            {
                                if (a[r - 1, c - 2] >= 0) //7
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c > 1)
                            {
                                if (a[r + 1, c - 2] >= 0)//8
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 2] = -3;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //knight
                        else if (a[r, c] == -4)//bishop
                        {
                            if(r < 7 && c < 7)
                            {
                                if (a[r + 1, c + 1] >= 0)
                                {
                                    if (r > c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8-pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c > 0)
                            {
                                if (a[r - 1, c - 1] >= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp+1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] >= 0)
                                {
                                    if (r < 8 - c)
                                        pp = r+1;
                                    else
                                        pp = 8 - c;
                                    z = 1;
                                    while (z < pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] >= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c+1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -4;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //bishop
                        else if (a[r, c] == -5)
                        {
                            if (r < 7)
                            {
                                if (a[r + 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-r)//down
                                    {
                                        if (a[r + z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0)
                            {
                                if (a[r - 1, c] >= 0)
                                {
                                    z = 1;
                                    while (z < r+1)//up
                                    {
                                        if (a[r - z, c] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c < 7)
                            {
                                if (a[r, c + 1] >= 0)
                                {
                                    z = 1;
                                    while (z < 8-c)//right
                                    {
                                        if (a[r, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (c > 0)
                            {
                                if (a[r, c - 1] >= 0)
                                {
                                    z = 1;
                                    while (z < c+1)//left
                                    {
                                        if (a[r, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }

                            }
                            if (r < 7 && c < 7)
                            {
                                if (a[r + 1, c + 1] >= 0)
                                {
                                    if (r > c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < 8-pp)//down right
                                    {
                                        if (a[r + z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c > 0)
                            {
                                if (a[r - 1, c - 1] >= 0)
                                {
                                    if (r < c)
                                        pp = r;
                                    else
                                        pp = c;
                                    z = 1;
                                    while (z < pp+1)//up left
                                    {
                                        if (a[r - z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] >= 0)
                                {
                                    if (r < 8 - c)
                                        pp = r+1;
                                    else
                                        pp = 8 - c;
                                    z = 1;
                                    while (z < pp)//up right
                                    {
                                        if (a[r - z, c + z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r - z, c + z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r - z, c + z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] >= 0)
                                {
                                    if (c < 8 - r)
                                        pp = c+1;
                                    else
                                        pp = 8 - r;
                                    z = 1;
                                    while (z < pp)//down left
                                    {
                                        if (a[r + z, c - z] == 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                        }
                                        else if (a[r + z, c - z] > 0)
                                        {
                                            a1[r, c] = 0;
                                            a1[r + z, c - z] = -5;
                                            check = B_check(a1);
                                            if (check == false)
                                            {
                                                if (mov == 1)
                                                    score1 = Score_B(a1);
                                                else
                                                {
                                                    score1 = Comp_w_scr(a1, mov - 1, score);
                                                }
                                                if (mov % 2 == 0)
                                                {
                                                    if (score1 < lastscore)
                                                        return score1;
                                                    if (score1 < score)
                                                        score = score1;
                                                }
                                                else
                                                {
                                                    if (score1 > lastscore)
                                                        return score1;
                                                    if (score1 > score)
                                                        score = score1;
                                                }
                                            }
                                            for (r2 = 0; r2 < a.GetLength(0); r2++)
                                            {
                                                for (c2 = 0; c2 < a.GetLength(1); c2++)
                                                {
                                                    a1[r2, c2] = a[r2, c2];
                                                }
                                            }
                                            z = 8;
                                        }
                                        else
                                            z = 8;  //break
                                        z++;
                                    }
                                }
                            }
                        } //queen
                        else if (a[r, c] == -6)
                        {
                            if (r < 7)
                            {
                                if (a[r + 1, c] >= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r + 1, c] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0)
                            {
                                if (a[r - 1, c] >= 0)
                                {
                                    a1[r, c] = 0;
                                    a1[r - 1, c] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c < 7)
                            {
                                if (a[r + 1, c + 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c + 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r < 7 && c > 0)
                            {
                                if (a[r + 1, c - 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r + 1, c - 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c < 7)
                            {
                                if (a[r - 1, c + 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c + 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (r > 0 && c > 0)
                            {
                                if (a[r - 1, c - 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r - 1, c - 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (c < 7)
                            {
                                if (a[r, c + 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c + 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                            if (c > 0)
                            {
                                if (a[r, c - 1] >= 0)
                                {
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                    a1[r, c] = 0;
                                    a1[r, c - 1] = -6;
                                    check = B_check(a1);
                                    if (check == false)
                                    {
                                        if (mov == 1)
                                            score1 = Score_B(a1);
                                        else
                                        {
                                            score1 = Comp_w_scr(a1, mov - 1, score);
                                        }
                                        if (mov % 2 == 0)
                                        {
                                            if (score1 < lastscore)
                                                return score1;
                                            if (score1 < score)
                                                score = score1;
                                        }
                                        else
                                        {
                                            if (score1 > lastscore)
                                                return score1;
                                            if (score1 > score)
                                                score = score1;
                                        }
                                    }
                                    for (r2 = 0; r2 < a.GetLength(0); r2++)
                                    {
                                        for (c2 = 0; c2 < a.GetLength(1); c2++)
                                        {
                                            a1[r2, c2] = a[r2, c2];
                                        }
                                    }
                                }
                            }
                        } //king 
                    }
                }
            }
            return score;
        }

        private void Computer_Click(object sender, EventArgs e)
        {
            int lp = 0;
            if (Turn_label.Text == "Turn: White")
            {
                Clean(b);
                rc = Comp_W(a, mov);
                r = rc[0];
                c = rc[1];
                r2 = rc[2];
                c2 = rc[3];
                if ((r==r2&&c==c2)|| a[r, c] == 0)
                {
                    rc = Comp_W(a, mov+1);
                    r = rc[0];
                    c = rc[1];
                    r2 = rc[2];
                    c2 = rc[3];
                }
                if (a[r, c] == 1 && r2 == 7)
                {
                    a[r2, c2] = 5;
                    b[r2, c2].BackgroundImage = w_queen;
                }
                else
                {
                    a[r2, c2] = a[r, c];
                    b[r2, c2].BackgroundImage = b[r, c].BackgroundImage;
                }
                a[r, c] = 0;
                b[r, c].ForeColor = Color.Blue;
                b[r, c].FlatAppearance.BorderSize = 3;
                b[r, c].BackgroundImage = null;
                b[r2, c2].ForeColor = Color.Yellow;
                b[r2, c2].FlatAppearance.BorderSize = 3;
                Turn_label.Text = "Turn: Black";
                for (r = 0; r < a.GetLength(0); r++)
                    for (c = 0; c < a.GetLength(1); c++)
                        if (Math.Abs(a[r, c]) == 6) { lp++; }
                if (lp != 2)
                {
                    MessageBox.Show("checkmate");
                    Application.Restart();
                }
                W_checkmate(a);
            }
            else
            {
                Clean(b);
                rc = Comp_B(a, mov);
                r = rc[0];
                c = rc[1];
                r2 = rc[2];
                c2 = rc[3];
                if ((r == r2 && c == c2)|| a[r, c] == 0)
                {
                    rc = Comp_B(a, mov + 1);
                    r = rc[0];
                    c = rc[1];
                    r2 = rc[2];
                    c2 = rc[3];
                }
                if (a[r, c] == -1 && r2 == 0)
                {
                    a[r2, c2] = -5;
                    b[r2, c2].BackgroundImage = b_queen;
                }
                else
                {
                    a[r2, c2] = a[r, c];
                    b[r2, c2].BackgroundImage = b[r, c].BackgroundImage;
                }
                a[r, c] = 0;
                b[r, c].ForeColor = Color.Blue;
                b[r, c].FlatAppearance.BorderSize = 3;
                b[r, c].BackgroundImage = null;
                b[r2, c2].ForeColor = Color.Yellow;
                b[r2, c2].FlatAppearance.BorderSize = 3;
                Turn_label.Text = "Turn: White";
                for (r = 0; r < a.GetLength(0); r++)
                    for (c = 0; c < a.GetLength(1); c++)
                        if (Math.Abs(a[r, c]) == 6) { lp++; }
                if (lp != 2)
                {
                    MessageBox.Show("checkmate");
                    Application.Restart();
                }
                B_checkmate(a);
            }
        }

        private void Restart_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Play.Enabled = false;
            Computer.Enabled = true;
            Turn_label.Text = "Turn: White"; //show turn
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
            #region Pawn //1
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
            #region rook //2
            a[0, 0] = 2;
            b[0, 0].BackgroundImage = w_rook;
            a[0, 7] = 2;
            b[0, 7].BackgroundImage = w_rook;
            a[7, 0] = -2;
            b[7, 0].BackgroundImage = b_rook;
            a[7, 7] = -2;
            b[7, 7].BackgroundImage = b_rook;
            #endregion 
            #region knights //3
            a[0, 1] = 3;
            b[0, 1].BackgroundImage = w_knight;
            a[0, 6] = 3;
            b[0, 6].BackgroundImage = w_knight;
            a[7, 1] = -3;
            b[7, 1].BackgroundImage = b_knight;
            a[7, 6] = -3;
            b[7, 6].BackgroundImage = b_knight;
            #endregion
            #region bishop//4
            a[0, 2] = 4;
            b[0, 2].BackgroundImage = w_bishop;
            a[0, 5] = 4;
            b[0, 5].BackgroundImage = w_bishop;
            a[7, 2] = -4;
            b[7, 2].BackgroundImage = b_bishop;
            a[7, 5] = -4;
            b[7, 5].BackgroundImage = b_bishop;
            #endregion
            #region queen //5
            a[0, 4] = 5;
            b[0, 4].BackgroundImage = w_queen;
            a[7, 4] = -5;
            b[7, 4].BackgroundImage = b_queen;
            #endregion
            #region king //6
            a[0, 3] = 6;
            b[0, 3].BackgroundImage = w_king;
            a[7, 3] = -6;
            b[7, 3].BackgroundImage = b_king;
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
                if (b[r, c].ForeColor == Color.Green || b[r, c].ForeColor == Color.Blue || b[r, c].ForeColor == Color.Red || (r2 == r && c2 == c))
                {
                    if (r2 == r && c2 == c)
                    {
                        Clean(b);
                        k = 0;
                        r2 = -1;
                        c2 = -1;
                    } //cancel
                    else
                    {
                        if (k == 1)
                        {
                            a[r, c] = 1;
                            b[r, c].BackgroundImage = w_pawn;
                            if (r == 7)
                            {
                                a[r, c] = 5;
                                b[r, c].BackgroundImage = w_queen;
                            }
                        } //pawn
                        else if (k == 2)
                        {
                            a[r, c] = 2;
                            b[r, c].BackgroundImage = w_rook;
                            white_casteling_firstmove = true;
                        } //rook
                        else if (k == 3)
                        {
                            a[r, c] = 3;
                            b[r, c].BackgroundImage = w_knight;
                        } //knight
                        else if (k == 4)
                        {
                            a[r, c] = 4;
                            b[r, c].BackgroundImage = w_bishop;
                        } //bishop
                        else if (k == 5)
                        {
                            a[r, c] = 5;
                            b[r, c].BackgroundImage = w_queen;
                        } //queen
                        else if (k == 6)
                        {
                            a[r, c] = 6;
                            b[r, c].BackgroundImage = w_king;
                            white_casteling_firstmove = true;
                        } //king
                        else if (k == 7)
                        {
                            if (c == 1)
                            {
                                b[r, c + 2].BackgroundImage = null;
                                b[r, c].BackgroundImage = w_king;
                                b[r, c - 1].BackgroundImage = null;
                                b[r, c + 1].BackgroundImage = w_rook;
                                a[r, c + 2] = 0;
                                a[r, c] = 6;
                                a[r, c + 1] = 2;
                                a[r, c - 1] = 0;
                            }
                            else if (c == 5)
                            {
                                b[r, c + 2].BackgroundImage = null;
                                b[r, c].BackgroundImage = w_king;
                                b[r, c - 1].BackgroundImage = w_rook;
                                b[r, c - 2].BackgroundImage = null;
                                a[r, c + 2] = 0;
                                a[r, c] = 6;
                                a[r, c - 1] = 2;
                                a[r, c - 2] = 0;
                            }
                            white_casteling_firstmove = true;
                        } //castling

                        a[r2, c2] = 0;
                        b[r2, c2].BackgroundImage = null;
                        B_checkmate(a);
                        Clean(b);
                        Turn_label.Text = "Turn: Black";
                        k = 0;
                    }
                }
                else if (a[r, c] > 0)
                {
                    Clean(b);
                    check1 = false;
                    if (a[r, c] == 1)
                    {
                        try
                        {
                            if (a[r + 1, c] == 0)
                            {
                                try
                                {
                                    if (r == 1 && a[r + 2, c] == 0)//first move
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + 1, c] = 1;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + 1, c].ForeColor = Color.Green;
                                            b[r + 1, c].FlatAppearance.BorderSize = 3;
                                            k = 1;
                                            r2 = r;
                                            c2 = c;
                                        }
                                        check1 = true;
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + 2, c] = 1;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + 2, c].ForeColor = Color.Green;
                                            b[r + 2, c].FlatAppearance.BorderSize = 3;
                                            k = 1;
                                            r2 = r;
                                            c2 = c;
                                        }
                                        check1 = true;
                                    }
                                    else
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + 1, c] = 1;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + 1, c].ForeColor = Color.Green;
                                            b[r + 1, c].FlatAppearance.BorderSize = 3;
                                            k = 1;
                                            r2 = r;
                                            c2 = c;
                                        }
                                        check1 = true;
                                    }
                                }
                                catch
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c] = 1;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c].ForeColor = Color.Green;
                                        b[r + 1, c].FlatAppearance.BorderSize = 3;
                                        k = 1;
                                        r2 = r;
                                        c2 = c;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c + 1] < 0)
                            {
                                for (i = 0; i < a.GetLength(0); i++)
                                {
                                    for (j = 0; j < a.GetLength(1); j++)
                                    {
                                        a1[i, j] = a[i, j];
                                    }
                                }
                                a1[r + 1, c + 1] = 1;
                                a1[r, c] = 0;
                                check1 = W_check(a1);
                                if (check1 == false)
                                {
                                    b[r + 1, c + 1].ForeColor = Color.Blue;
                                    b[r + 1, c + 1].FlatAppearance.BorderSize = 3;
                                    k = 1;
                                    r2 = r;
                                    c2 = c;
                                }
                                check1 = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 1] < 0)
                            {
                                for (i = 0; i < a.GetLength(0); i++)
                                {
                                    for (j = 0; j < a.GetLength(1); j++)
                                    {
                                        a1[i, j] = a[i, j];
                                    }
                                }
                                a1[r + 1, c - 1] = 1;
                                a1[r, c] = 0;
                                check1 = W_check(a1);
                                if (check1 == false)
                                {
                                    b[r + 1, c - 1].ForeColor = Color.Blue;
                                    b[r + 1, c - 1].FlatAppearance.BorderSize = 3;
                                    k = 1;
                                    r2 = r;
                                    c2 = c;
                                }
                                check1 = true;
                            }
                        }
                        catch { }
                    } //pawn
                    else if (a[r, c] == 2)//rook
                    {
                        try
                        {
                            if (a[r + 1, c] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//down
                                {
                                    if (a[r + z, c] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Green;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Blue;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//up
                                {
                                    if (a[r - z, c] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Green;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Blue;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c + 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//right
                                {
                                    if (a[r, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Green;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c + z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Blue;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c - 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//left
                                {
                                    if (a[r, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Green;
                                            b[r, c - z].FlatAppearance.BorderSize = 3; ;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c - z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = 2;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Blue;
                                            b[r, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                    } //rook
                    else if (a[r, c] == 3)
                    {
                        try
                        {
                            if (a[r + 2, c + 1] <= 0) //1
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 2, c + 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c + 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c + 1].ForeColor = Color.Green;
                                        b[r + 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c + 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c + 1].ForeColor = Color.Blue;
                                        b[r + 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 2, c - 1] <= 0) //2
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 2, c - 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c - 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c - 1].ForeColor = Color.Green;
                                        b[r + 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c - 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c - 1].ForeColor = Color.Blue;
                                        b[r + 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 2, c + 1] <= 0)//3
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 2, c + 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c + 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c + 1].ForeColor = Color.Green;
                                        b[r - 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c + 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c + 1].ForeColor = Color.Blue;
                                        b[r - 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 2, c - 1] <= 0)//4
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 2, c - 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c - 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c - 1].ForeColor = Color.Green;
                                        b[r - 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c - 1] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c - 1].ForeColor = Color.Blue;
                                        b[r - 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c + 2] <= 0)//5
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 1, c + 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c + 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c + 2].ForeColor = Color.Green;
                                        b[r + 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c + 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c + 2].ForeColor = Color.Blue;
                                        b[r + 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 2] <= 0)//6
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 1, c + 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c + 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c + 2].ForeColor = Color.Green;
                                        b[r - 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c + 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c + 2].ForeColor = Color.Blue;
                                        b[r - 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 2] <= 0)//7
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 1, c - 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c - 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c - 2].ForeColor = Color.Green;
                                        b[r - 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c - 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c - 2].ForeColor = Color.Blue;
                                        b[r - 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 2] <= 0)//8
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 1, c - 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c - 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c - 2].ForeColor = Color.Green;
                                        b[r + 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c - 2] = 3;
                                    a1[r, c] = 0;
                                    check1 = W_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c - 2].ForeColor = Color.Blue;
                                        b[r + 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                    } //knight
                    else if (a[r, c] == 4)//bishop
                    {
                        try
                        {
                            if (a[r + 1, c + 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//down right
                                {
                                    if (a[r + z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Green;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c + z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Blue;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//up left
                                {
                                    if (a[r - z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Green;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c - z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Blue;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//up right
                                {
                                    if (a[r - z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Green;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c + z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Blue;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//down left
                                {
                                    if (a[r + z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Green;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c - z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = 4;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Blue;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                    } //bishop
                    else if (a[r, c] == 5)
                    {
                        try
                        {
                            if (a[r + 1, c] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//down
                                {
                                    if (a[r + z, c] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Green;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Blue;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//up
                                {
                                    if (a[r - z, c] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Green;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Blue;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c + 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//right
                                {
                                    if (a[r, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Green;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c + z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Blue;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c - 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//left
                                {
                                    if (a[r, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Green;
                                            b[r, c - z].FlatAppearance.BorderSize = 3; ;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c - z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Blue;
                                            b[r, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c + 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//down right
                                {
                                    if (a[r + z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Green;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c + z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Blue;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//up left
                                {
                                    if (a[r - z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Green;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c - z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Blue;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//up right
                                {
                                    if (a[r - z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Green;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c + z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Blue;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 1] <= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//down left
                                {
                                    if (a[r + z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Green;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c - z] < 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = 5;
                                        a1[r, c] = 0;
                                        check1 = W_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Blue;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                    } //queen
                    else if (a[r, c] == 6)
                    {
                        Boolean check = false;
                        try
                        {
                            if (a[r + 1, c] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r + 1, c] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    b[r + 1, c].FlatAppearance.BorderSize = 3;
                                    if (a[r + 1, c] < 0)
                                        b[r + 1, c].ForeColor = Color.Blue;
                                    else
                                        b[r + 1, c].ForeColor = Color.Green;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r - 1, c] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r - 1, c] < 0)
                                        b[r - 1, c].ForeColor = Color.Blue;
                                    else
                                        b[r - 1, c].ForeColor = Color.Green;
                                    b[r - 1, c].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c + 1] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r + 1, c + 1] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r + 1, c + 1] < 0)
                                        b[r + 1, c + 1].ForeColor = Color.Blue;
                                    else
                                        b[r + 1, c + 1].ForeColor = Color.Green;
                                    b[r + 1, c + 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 1] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r + 1, c - 1] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r + 1, c - 1] < 0)
                                        b[r + 1, c - 1].ForeColor = Color.Blue;
                                    else
                                        b[r + 1, c - 1].ForeColor = Color.Green;
                                    b[r + 1, c - 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 1] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r - 1, c + 1] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r - 1, c + 1] < 0)
                                        b[r - 1, c + 1].ForeColor = Color.Blue;
                                    else
                                        b[r - 1, c + 1].ForeColor = Color.Green;
                                    b[r - 1, c + 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 1] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r - 1, c - 1] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r - 1, c - 1] < 0)
                                        b[r - 1, c - 1].ForeColor = Color.Blue;
                                    else
                                        b[r - 1, c - 1].ForeColor = Color.Green;
                                    b[r - 1, c - 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c + 1] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c + 1] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r, c + 1] < 0)
                                        b[r, c + 1].ForeColor = Color.Blue;
                                    else
                                        b[r, c + 1].ForeColor = Color.Green;
                                    b[r, c + 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c - 1] <= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c - 1] = 6;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r, c - 1] < 0)
                                        b[r, c - 1].ForeColor = Color.Blue;
                                    else
                                        b[r, c - 1].ForeColor = Color.Green;
                                    b[r, c - 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (r == 0 && c == 3 && a[r, c - 1] == 0 && a[r, c - 2] == 0 && a[r, c - 3] == 2 && white_casteling_firstmove == false)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c - 2] = 6;
                                a1[r, c - 1] = 2;
                                a1[r, c - 3] = 0;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 7;
                                    b[r, c - 2].ForeColor = Color.Red;
                                    b[r, c - 2].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (r == 0 && c == 3 && a[r, c + 1] == 0 && a[r, c + 2] == 0 && a[r, c + 3] == 0 && a[r, c + 4] == 2 && white_casteling_firstmove == false)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c + 2] = 6;
                                a1[r, c + 1] = 2;
                                a1[r, c + 4] = 0;
                                check = W_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 7;
                                    b[r, c + 2].ForeColor = Color.Red;
                                    b[r, c + 2].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                    } //king
                }
            }
            else if (Turn_label.Text == "Turn: Black")
            {
                if (b[r, c].ForeColor == Color.Green || b[r, c].ForeColor == Color.Blue || b[r, c].ForeColor == Color.Red || (r2 == r && c2 == c))
                {
                    if (r2 == r && c2 == c)
                    {
                        Clean(b);
                        k = 0;
                        r2 = -1;
                        c2 = -1;
                    } //cancel
                    else
                    {
                        if (k == 1)
                        {
                            a[r, c] = -1;
                            b[r, c].BackgroundImage = b_pawn;
                            if (r == 0)
                            {
                                a[r, c] = -5;
                                b[r, c].BackgroundImage = b_queen;
                            }
                        } //pawn
                        else if (k == 2)
                        {
                            a[r, c] = -2;
                            b[r, c].BackgroundImage = b_rook;
                            black_casteling_firstmove = true;
                        } //rook
                        else if (k == 3)
                        {
                            a[r, c] = -3;
                            b[r, c].BackgroundImage = b_knight;
                        } //knight
                        else if (k == 4)
                        {
                            a[r, c] = -4;
                            b[r, c].BackgroundImage = b_bishop;
                        } //bishop
                        else if (k == 5)
                        {
                            a[r, c] = -5;
                            b[r, c].BackgroundImage = b_queen;
                        } //queen
                        else if (k == 6)
                        {
                            a[r, c] = -6;
                            b[r, c].BackgroundImage = b_king;
                            black_casteling_firstmove = true;
                        } //king
                        else if (k == 7)
                        {
                            if (c == 1)
                            {
                                b[r, c + 2].BackgroundImage = null;
                                b[r, c].BackgroundImage = b_king;
                                b[r, c - 1].BackgroundImage = null;
                                b[r, c + 1].BackgroundImage = b_rook;
                                a[r, c + 2] = 0;
                                a[r, c] = -6;
                                a[r, c + 1] = -2;
                                a[r, c - 1] = 0;
                            }
                            else if (c == 5)
                            {
                                b[r, c + 2].BackgroundImage = null;
                                b[r, c].BackgroundImage = b_king;
                                b[r, c - 1].BackgroundImage = b_rook;
                                b[r, c - 2].BackgroundImage = null;
                                a[r, c + 2] = 0;
                                a[r, c] = -6;
                                a[r, c - 1] = -2;
                                a[r, c - 2] = 0;
                            }
                            black_casteling_firstmove = true;
                        } //castling

                        a[r2, c2] = 0;
                        b[r2, c2].BackgroundImage = null;
                        W_checkmate(a);
                        Clean(b);
                        Turn_label.Text = "Turn: White";
                        k = 0;
                    }
                }
                else if (a[r, c] < 0)
                {
                    Clean(b);
                    check1 = false;
                    if (a[r, c] == -1)
                    {
                        try
                        {
                            if (a[r - 1, c] == 0)
                            {
                                try
                                {
                                    if (r == 6 && a[r - 2, c] == 0)//first move
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - 1, c] = -1;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - 1, c].ForeColor = Color.Green;
                                            b[r - 1, c].FlatAppearance.BorderSize = 3;
                                            k = 1;
                                            r2 = r;
                                            c2 = c;
                                        }
                                        check1 = true;
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - 2, c] = -1;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - 2, c].ForeColor = Color.Green;
                                            b[r - 2, c].FlatAppearance.BorderSize = 3;
                                            k = 1;
                                            r2 = r;
                                            c2 = c;
                                        }
                                        check1 = false;
                                    }
                                    else
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - 1, c] = -1;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - 1, c].ForeColor = Color.Green;
                                            b[r - 1, c].FlatAppearance.BorderSize = 3;
                                            k = 1;
                                            r2 = r;
                                            c2 = c;
                                        }
                                        check1 = true;
                                    }
                                }
                                catch
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c] = -1;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c].ForeColor = Color.Green;
                                        b[r - 1, c].FlatAppearance.BorderSize = 3;
                                        k = 1;
                                        r2 = r;
                                        c2 = c;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 1] > 0)
                            {
                                for (i = 0; i < a.GetLength(0); i++)
                                {
                                    for (j = 0; j < a.GetLength(1); j++)
                                    {
                                        a1[i, j] = a[i, j];
                                    }
                                }
                                a1[r - 1, c + 1] = -1;
                                a1[r, c] = 0;
                                check1 = B_check(a1);
                                if (check1 == false)
                                {
                                    b[r - 1, c + 1].ForeColor = Color.Blue;
                                    b[r - 1, c + 1].FlatAppearance.BorderSize = 3;
                                    k = 1;
                                    r2 = r;
                                    c2 = c;
                                }
                                check1 = true;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 1] > 0)
                            {
                                for (i = 0; i < a.GetLength(0); i++)
                                {
                                    for (j = 0; j < a.GetLength(1); j++)
                                    {
                                        a1[i, j] = a[i, j];
                                    }
                                }
                                a1[r - 1, c - 1] = -1;
                                a1[r, c] = 0;
                                check1 = B_check(a1);
                                if (check1 == false)
                                {
                                    b[r - 1, c - 1].ForeColor = Color.Blue;
                                    b[r - 1, c - 1].FlatAppearance.BorderSize = 3;
                                    k = 1;
                                    r2 = r;
                                    c2 = c;
                                }
                                check1 = true;
                            }
                        }
                        catch { }
                    } //pawn
                    else if (a[r, c] == -2)//rook
                    {
                        try
                        {
                            if (a[r + 1, c] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//down
                                {
                                    if (a[r + z, c] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Green;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Blue;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//up
                                {
                                    if (a[r - z, c] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Green;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Blue;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c + 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//right
                                {
                                    if (a[r, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Green;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c + z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Blue;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c - 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 2;
                                z = 1;
                                while (z < 8)//left
                                {
                                    if (a[r, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Green;
                                            b[r, c - z].FlatAppearance.BorderSize = 3; ;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c - z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = -2;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Blue;
                                            b[r, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                    } //rook
                    else if (a[r, c] == -3)
                    {
                        try
                        {
                            if (a[r + 2, c + 1] >= 0) //1
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 2, c + 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c + 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c + 1].ForeColor = Color.Green;
                                        b[r + 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c + 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c + 1].ForeColor = Color.Blue;
                                        b[r + 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 2, c - 1] >= 0) //2
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 2, c - 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c - 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c - 1].ForeColor = Color.Green;
                                        b[r + 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 2, c - 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 2, c - 1].ForeColor = Color.Blue;
                                        b[r + 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 2, c + 1] >= 0)//3
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 2, c + 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c + 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c + 1].ForeColor = Color.Green;
                                        b[r - 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c + 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c + 1].ForeColor = Color.Blue;
                                        b[r - 2, c + 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 2, c - 1] >= 0)//4
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 2, c - 1] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c - 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c - 1].ForeColor = Color.Green;
                                        b[r - 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 2, c - 1] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 2, c - 1].ForeColor = Color.Blue;
                                        b[r - 2, c - 1].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c + 2] >= 0)//5
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 1, c + 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c + 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c + 2].ForeColor = Color.Green;
                                        b[r + 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c + 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c + 2].ForeColor = Color.Blue;
                                        b[r + 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 2] >= 0)//6
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 1, c + 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c + 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c + 2].ForeColor = Color.Green;
                                        b[r - 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c + 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c + 2].ForeColor = Color.Blue;
                                        b[r - 1, c + 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 2] >= 0)//7
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r - 1, c - 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c - 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c - 2].ForeColor = Color.Green;
                                        b[r - 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r - 1, c - 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r - 1, c - 2].ForeColor = Color.Blue;
                                        b[r - 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 2] >= 0)//8
                            {
                                r2 = r;
                                c2 = c;
                                k = 3;
                                if (a[r + 1, c - 2] == 0)
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c - 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c - 2].ForeColor = Color.Green;
                                        b[r + 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                                else
                                {
                                    for (i = 0; i < a.GetLength(0); i++)
                                    {
                                        for (j = 0; j < a.GetLength(1); j++)
                                        {
                                            a1[i, j] = a[i, j];
                                        }
                                    }
                                    a1[r + 1, c - 2] = -3;
                                    a1[r, c] = 0;
                                    check1 = B_check(a1);
                                    if (check1 == false)
                                    {
                                        b[r + 1, c - 2].ForeColor = Color.Blue;
                                        b[r + 1, c - 2].FlatAppearance.BorderSize = 3;
                                    }
                                    check1 = true;
                                }
                            }
                        }
                        catch { }
                    } //knight
                    else if (a[r, c] == -4)//bishop
                    {
                        try
                        {
                            if (a[r + 1, c + 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//down right
                                {
                                    if (a[r + z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Green;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c + z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Blue;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//up left
                                {
                                    if (a[r - z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Green;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c - z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Blue;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//up right
                                {
                                    if (a[r - z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Green;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c + z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Blue;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 4;
                                z = 1;
                                while (z < 8)//down left
                                {
                                    if (a[r + z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Green;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c - z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = -4;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Blue;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                    } //bishop
                    else if (a[r, c] == -5)
                    {
                        try
                        {
                            if (a[r + 1, c] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//down
                                {
                                    if (a[r + z, c] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Green;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c].ForeColor = Color.Blue;
                                            b[r + z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//up
                                {
                                    if (a[r - z, c] == 0)
                                    {

                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Green;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c].ForeColor = Color.Blue;
                                            b[r - z, c].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c + 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//right
                                {
                                    if (a[r, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Green;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c + z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c + z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c + z].ForeColor = Color.Blue;
                                            b[r, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c - 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//left
                                {
                                    if (a[r, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Green;
                                            b[r, c - z].FlatAppearance.BorderSize = 3; ;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r, c - z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r, c - z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r, c - z].ForeColor = Color.Blue;
                                            b[r, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c + 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//down right
                                {
                                    if (a[r + z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Green;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c + z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c + z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c + z].ForeColor = Color.Blue;
                                            b[r + z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//up left
                                {
                                    if (a[r - z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Green;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c - z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c - z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c - z].ForeColor = Color.Blue;
                                            b[r - z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//up right
                                {
                                    if (a[r - z, c + z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Green;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r - z, c + z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r - z, c + z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r - z, c + z].ForeColor = Color.Blue;
                                            b[r - z, c + z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 1] >= 0)
                            {
                                r2 = r;
                                c2 = c;
                                k = 5;
                                z = 1;
                                while (z < 8)//down left
                                {
                                    if (a[r + z, c - z] == 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Green;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                    }
                                    else if (a[r + z, c - z] > 0)
                                    {
                                        for (i = 0; i < a.GetLength(0); i++)
                                        {
                                            for (j = 0; j < a.GetLength(1); j++)
                                            {
                                                a1[i, j] = a[i, j];
                                            }
                                        }
                                        a1[r + z, c - z] = -5;
                                        a1[r, c] = 0;
                                        check1 = B_check(a1);
                                        if (check1 == false)
                                        {
                                            b[r + z, c - z].ForeColor = Color.Blue;
                                            b[r + z, c - z].FlatAppearance.BorderSize = 3;
                                        }
                                        check1 = true;
                                        z = 8;
                                    }
                                    else
                                    { z = 8; } //break
                                    z++;
                                }
                            }
                        }
                        catch { }
                    } //queen
                    else if (a[r, c] == -6)
                    {
                        Boolean check = false;
                        try
                        {
                            if (a[r + 1, c] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r + 1, c] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r + 1, c] > 0)
                                        b[r + 1, c].ForeColor = Color.Blue;
                                    else
                                        b[r + 1, c].ForeColor = Color.Green;
                                    b[r + 1, c].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r - 1, c] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r - 1, c] > 0)
                                        b[r - 1, c].ForeColor = Color.Blue;
                                    else
                                        b[r - 1, c].ForeColor = Color.Green;
                                    b[r - 1, c].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c + 1] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r + 1, c + 1] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r + 1, c + 1] > 0)
                                        b[r + 1, c + 1].ForeColor = Color.Blue;
                                    else
                                        b[r + 1, c + 1].ForeColor = Color.Green;
                                    b[r + 1, c + 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r + 1, c - 1] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r + 1, c - 1] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r + 1, c - 1] > 0)
                                        b[r + 1, c - 1].ForeColor = Color.Blue;
                                    else
                                        b[r + 1, c - 1].ForeColor = Color.Green;
                                    b[r + 1, c - 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c + 1] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r - 1, c + 1] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r - 1, c + 1] > 0)
                                        b[r - 1, c + 1].ForeColor = Color.Blue;
                                    else
                                        b[r - 1, c + 1].ForeColor = Color.Green;
                                    b[r - 1, c + 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r - 1, c - 1] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r - 1, c - 1] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r - 1, c - 1] > 0)
                                        b[r - 1, c - 1].ForeColor = Color.Blue;
                                    else
                                        b[r - 1, c - 1].ForeColor = Color.Green;
                                    b[r - 1, c - 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c + 1] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c + 1] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r, c + 1] > 0)
                                        b[r, c + 1].ForeColor = Color.Blue;
                                    else
                                        b[r, c + 1].ForeColor = Color.Green;
                                    b[r, c + 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (a[r, c - 1] >= 0)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c - 1] = -6;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 6;
                                    if (a[r, c - 1] > 0)
                                        b[r, c - 1].ForeColor = Color.Blue;
                                    else
                                        b[r, c - 1].ForeColor = Color.Green;
                                    b[r, c - 1].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (r == 7 && c == 3 && a[r, c - 1] == 0 && a[r, c - 2] == 0 && a[r, c - 3] == -2 && black_casteling_firstmove == false)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c - 2] = -6;
                                a1[r, c - 1] = -2;
                                a1[r, c - 3] = 0;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 7;
                                    b[r, c - 2].ForeColor = Color.Red;
                                    b[r, c - 2].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                        try
                        {
                            if (r == 7 && c == 3 && a[r, c + 1] == 0 && a[r, c + 2] == 0 && a[r, c + 3] == 0 && a[r, c + 4] == -2 && black_casteling_firstmove == false)
                            {
                                for (i = 0; i < 8; i++)
                                    for (j = 0; j < 8; j++)
                                        a1[i, j] = a[i, j];
                                a1[r, c] = 0;
                                a1[r, c + 2] = -6;
                                a1[r, c + 1] = -2;
                                a1[r, c + 4] = 0;
                                check = B_check(a1);
                                if (check == false)
                                {
                                    r2 = r;
                                    c2 = c;
                                    k = 7;
                                    b[r, c + 2].ForeColor = Color.Red;
                                    b[r, c + 2].FlatAppearance.BorderSize = 3;
                                }
                                check = false;
                            }
                        }
                        catch { }
                    } //king
                }
            }
        }
    }
}