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

        private int _xn = 10;
        private int _yn = 5;
        private int _otx = 15;
        private int _oty = 30;
        private int _w;
        private int _h;
        private int _sx;
        private int _sy;

        public Form1()
        {
            InitializeComponent();            
            _h = this.Size.Height - 39;
            _w = this.Size.Width;
            _sx = (this.Size.Width - 15 - 2 * _otx) / _xn;
            _sy = (this.Size.Height - 39 - 2 * _oty) / _yn;
        }

        public void DrawRectangleInt(PaintEventArgs e)
        {

            Pen blackPen = new Pen(Color.Black, 1);
           
            for (int i = 0; i < _xn; i++)
            {
                for (int j =0; j < _yn; j++)
                {
                    e.Graphics.DrawRectangle(blackPen, _otx + _sx * i, _oty + _sy * j, _sx, _sy);
                }
            }          
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PaintEventArgs args = new PaintEventArgs(this.CreateGraphics(), new Rectangle(0, 0, _w, _h));
            DrawRectangleInt(args);
            drawInCell(args, 2, 2);
        }

        public void drawInCell(PaintEventArgs e,int x,int y)
        {
            Pen blackPen = new Pen(Color.Blue, 5);

            e.Graphics.DrawRectangle(blackPen, _otx + _sx * x + _sx/3, _oty + _sy * y + _sy / 3, _sx/3, _sy/3);
        }
    }
}
