// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StrategyGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            


        }

        public void DrawRectangleInt(PaintEventArgs e,int xn,int yn, int otx, int oty)
        {

            Pen blackPen = new Pen(Color.Black, 1);
            int sx = (this.Size.Width -15- 2*otx) / xn;
            int sy = (this.Size.Height - 39 - 2*oty) / yn;
            for (int i = 0; i < xn; i++)
            {
                for (int j =0; j < yn; j++)
                {
                    e.Graphics.DrawRectangle(blackPen, otx + sx * i, oty + sy * j, sx, sy);
                }
            }          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int xn = 10;
            int yn = 10;
            int otx = 15;
            int oty = 30;
            int h = this.Size.Height - 39;
            int w = this.Size.Width;

            PaintEventArgs args = new PaintEventArgs(this.CreateGraphics(), new Rectangle(0, 0, w, h));
            DrawRectangleInt(args, xn, yn, otx,oty);
        }
    }
}
