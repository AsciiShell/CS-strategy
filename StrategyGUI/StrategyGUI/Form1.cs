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

        private int _xn;
        private int _yn;
        private int _otx;
        private int _oty;
        private int _w;
        private int _h;
        private int _sx;
        private int _sy;

        public Form1()
        {
             _xn = 20;
             _yn = 9;
             _otx = 15;
             _oty = 30;
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

        public void drawInCell(PaintEventArgs e,int x,int y)
        {
            Pen blackPen = new Pen(Color.Blue, 5);

            e.Graphics.DrawRectangle(blackPen, _otx + _sx * x + _sx/3, _oty + _sy * y + _sy / 3, _sx/3, _sy/3);
        }

        public void drawM(PaintEventArgs e, int x, int y, Pen p)
        {
            Graphics gr = e.Graphics;
             //= new Pen(Color.Blue, _sy/12);// цвет линии и ширина
            int sh = 8, vi = 8;

            Point p1 = new Point(x * _sx + _otx + _sx * (10 - sh) / 10, y * _sy + _oty + vi * _sy / 10);// первая точка
            Point p2 = new Point(x * _sx + _otx + _sx * (10 - sh) / 10, y * _sy + _oty + (10 - vi) * _sy / 10);// вторая точка
            Point p3 = new Point(x * _sx + _otx + 2 * _sx / 4, y * _sy + _oty + 1 * _sy / 2);
            Point p4 = new Point(x * _sx + _otx + _sx * sh / 10, y * _sy + _oty + (10 - vi) * _sy / 10);
            Point p5 = new Point(x * _sx + _otx + _sx * sh / 10, y * _sy + _oty + vi * _sy / 10);

            gr.DrawLine(p, p1, p2);// рисуем линию
            gr.DrawLine(p, p3, p2);
            gr.DrawLine(p, p3, p4);
            gr.DrawLine(p, p5, p4);
           // gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }

        public void drawT(PaintEventArgs e, int x, int y, Pen pp)
        {
            Graphics gr = e.Graphics;
            // = new Pen(Color.Blue, _sy/12);// цвет линии и ширина
            int sh = 8, vi = 8;

            Point p1 = new Point(x * _sx + _otx + _sx * (10 - sh) / 10, y * _sy + _oty + (10 - vi) * _sy / 10);// первая точка
            Point p2 = new Point(x * _sx + _otx + _sx * sh / 10, y * _sy + _oty + (10 - vi) * _sy / 10);// вторая точка

            Point p3 = new Point(x * _sx + _otx + _sx / 2, y * _sy + _oty + (10 - vi) * _sy / 10);
            Point p4 = new Point(x * _sx + _otx + _sx / 2, y * _sy + _oty + vi * _sy / 10);

            gr.DrawLine(pp, p1, p2);// рисуем линию
            gr.DrawLine(pp, p3, p4);
            //gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }

        public void drawP(PaintEventArgs e, int x, int y, Pen pp)
        {
            Graphics gr = e.Graphics;
            // = new Pen(Color.Blue, _sy/12);// цвет линии и ширина
            int sh = 7, vi = 8;

            Point p1 = new Point(x * _sx + _otx + _sx * (10 - sh) / 10, y * _sy + _oty + (10 - vi) * _sy / 10);// первая точка
            Point p2 = new Point(x * _sx + _otx + _sx * sh / 10, y * _sy + _oty + (10 - vi) * _sy / 10);// вторая точка

            Point p3 = new Point(x * _sx + _otx + (10 - sh) * _sx / 10, y * _sy + _oty + vi * _sy / 10);
            Point p4 = new Point(x * _sx + _otx + sh * _sx / 10, y * _sy + _oty + vi * _sy / 10);

            gr.DrawLine(pp, p3, p1);
            gr.DrawLine(pp, p1, p2);// рисуем линию
            gr.DrawLine(pp, p2, p4);
            //gr.Dispose();// освобождаем все ресурсы, связанные с отрисовкой
        }
        /* private void button2_Click(object sender, EventArgs e)
         {

         }*/

        private void button3_Click(object sender, EventArgs e)
        {
            PaintEventArgs args = new PaintEventArgs(this.CreateGraphics(), new Rectangle(0, 0, _w, _h));
            DrawRectangleInt(args);
            //drawInCell(args, 2, 2);
            Pen p = new Pen(Color.Blue, _sy / 12);
            //int mass[9][2] ;
            drawM(args, 0, 1,p);
            drawM(args, 0, 4, p);
            drawM(args, 0, 7, p);

            drawP(args, 2, 2,p);
            drawP(args, 2, 4, p);
            drawP(args, 2, 6, p);

            drawT(args, 4,1,p);
            drawT(args, 4,4, p);
            drawT(args, 4, 7, p);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;
            x = e.Location.X;
            y = e.Location.Y;

            if (x < _otx || y < _oty || x > _sx * (_xn + 1) || y > _sy * (_yn + 1))
            {
                label1.Text = "  ";
            }
            else
            {
                int iX = (x - _otx) / _sx;
                int iY = (y - _oty) / _sy;
                label1.Text = iX + "  " + iY;
            }
        }
    }
    
}
