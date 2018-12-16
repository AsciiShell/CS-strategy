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
using GameLibrary;
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
        private int _selX;
        private int _selY;
        private PaintEventArgs _args;
        private GameServer gameServer;

        public Form1()
        {
            gameServer = new GameServer(new Player(Player.Kind.USER, "User1"), new Player(Player.Kind.BOT, "BOT1"));
             _xn = 20;
             _yn = 9;
             _otx = 15;
             _oty = 30;

            _selX = -1;
            _selY = -1;
            InitializeComponent();
            _h = this.Size.Height - 39;
            _w = this.Size.Width;
            _sx = (this.Size.Width - 15 - 2 * _otx) / _xn;
            _sy = (this.Size.Height - 39 - 2 * _oty) / _yn;
            _args  = new PaintEventArgs(this.CreateGraphics(), new Rectangle(0, 0, _w, _h));
        }

        private void DrawRectangleInt(PaintEventArgs e)
        {

            Pen blackPen = new Pen(Color.Black, 1);
            Pen greenPen = new Pen(Color.Red, 3);

            for (int i = 0; i < _xn; i++)
            {
                for (int j = 0; j < _yn; j++)
                {
                    e.Graphics.DrawRectangle(blackPen, _otx + _sx * i, _oty + _sy * j, _sx, _sy);

                }
            }
            if (_selX != -1 && _selY != -1)
                e.Graphics.DrawRectangle(greenPen, _otx + _sx * _selX + 1, _oty + _sy * _selY + 1, _sx - 2, _sy - 2);

        }

        private void DrawUnits(PaintEventArgs e)
        {
            foreach (Cell item in gameServer.Players[0].Army)
            {
                item.Draw(e, _sx, _sy, _otx, _oty);
            }
            foreach (Cell item in gameServer.Players[1].Army)
            {
                item.Draw(e, _sx, _sy, _otx, _oty);
            }
        }



        /* private void button2_Click(object sender, EventArgs e)
         {

         }*/

        private void button3_Click(object sender, EventArgs e)
        {
            _args.Dispose();
            paintAll();
            
        }

        private void paintAll()
        {
            _h = this.Size.Height - 39;
            _w = this.Size.Width;
            _sx = (this.Size.Width - 15 - 2 * _otx) / _xn;
            _sy = (this.Size.Height - 39 - 2 * _oty) / _yn;
            _args = new PaintEventArgs(this.CreateGraphics(), new Rectangle(0, 0, _w, _h));
            DrawRectangleInt(_args);
            DrawUnits(_args);
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            int x, y;
            x = e.Location.X;
            y = e.Location.Y;

            if (x < _otx || y < _oty || x > _sx * (_xn ) || y > _sy * (_yn +1))
            {
                label1.Text = "  ";
                _selX = -1;
                _selY = -1;
                DrawRectangleInt(_args);
            }
            else
            {
                int iX = (x - _otx) / _sx;
                int iY = (y - _oty) / _sy;
                label1.Text = iX + "  " + iY;

                _selX = iX;
                _selY = iY;
                DrawRectangleInt(_args);
            }
        }
    }
    
}
