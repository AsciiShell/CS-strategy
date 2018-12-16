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
        private int _selX1;
        private int _selY1;
        private int _selX2;
        private int _selY2;

        private GameServer gameServer;

        public Form1()
        {
            gameServer = new GameServer(new Player(Player.Kind.USER, "User1"), new Player(Player.Kind.BOT, "BOT1"));
            _xn = 20;
            _yn = 9;
            _otx = 15;
            _oty = 30;

            _selX1 = -1;
            _selY1 = -1;
            _selX2 = -1;
            _selY2 = -1;
            InitializeComponent();
            _h = this.Size.Height - 39;
            _w = this.Size.Width;
            _sx = (this.Size.Width - 15 - 2 * _otx) / _xn;
            _sy = (this.Size.Height - 39 - 2 * _oty) / _yn;
            Notifer.Subscribe(paintAll, 500);
        }

        private void DrawRectangleInt(PaintEventArgs e)
        {

            Pen blackPen = new Pen(Color.Black, 1);
            Pen redPen = new Pen(Color.Red, 3);
            Pen bluePen = new Pen(Color.Blue, 3);

            for (int i = 0; i < _xn; i++)
            {
                for (int j = 0; j < _yn; j++)
                {
                    e.Graphics.DrawRectangle(blackPen, _otx + _sx * i, _oty + _sy * j, _sx, _sy);
                }
            }
            if (_selX1 != -1 && _selY1 != -1)
                e.Graphics.DrawRectangle(redPen, _otx + _sx * _selX1 + 1, _oty + _sy * _selY1 + 1, _sx - 2, _sy - 2);
            if (_selX2 != -1 && _selY2 != -1)
                e.Graphics.DrawRectangle(bluePen, _otx + _sx * _selX2 + 1, _oty + _sy * _selY2 + 1, _sx - 2, _sy - 2);

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
            
            paintAll();            
        }
        private bool paintAll()
        {
            
            _h = this.Size.Height - 39;
            _w = this.Size.Width;
            _sx = (this.Size.Width - 15 - 2 * _otx) / _xn;
            _sy = (this.Size.Height - 39 - 2 * _oty) / _yn;
            PaintEventArgs args = new PaintEventArgs(this.CreateGraphics(), new Rectangle(0, 0, _w, _h));
            args.Graphics.DrawRectangle(new Pen(Color.Green, this.Height ), 0,0, this.Width, this.Height);
            DrawRectangleInt(args);
            DrawUnits(args);
            args.Graphics.Dispose();
                return gameServer.IsEnabled;
        }
        /*private void paintAroundCell(int x, int y)
        {
            PaintEventArgs args = new PaintEventArgs(this.CreateGraphics(), new Rectangle((x-1) * _sx + _otx, (y - 1) * _sy + _oty, (x + 1) * _sx + _otx, (y + 1) * _sy + _oty));
            args.Graphics.DrawRectangle(new Pen(Color.Green, _sx * 3/2),
                (x - 1) * _sx + _otx + _sx * 3 / 2, (y - 1) * _sy + _oty + _sx * 3 / 2,
                (x ) * _sx + _otx - _sx * 3 / 2, (y ) * _sy + _oty - _sx * 3 / 2);

        }*/

        

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                _selX1 = -1;
                _selY1 = -1;
                _selX2 = -1;
                _selY2 = -1;
                paintAll();
            }
            else { 
                int x, y;
            x = e.Location.X;
            y = e.Location.Y;

                if (x < _otx || y < _oty || x > _sx * (_xn) || y > _sy * (_yn + 1))
                {
                    //label1.Text = "  ";
                    //_selX1 = -1;
                    //_selY1= -1;
                    //paintAll();
                }
                else
                {
                    int iX = (x - _otx) / _sx;
                    int iY = (y - _oty) / _sy;
                    label1.Text = iX + "  " + iY;
                    if ((_selX1 == -1 && _selY1 == -1) || (_selX1 == iX && _selY1 == iY))
                    {
                        
                        _selX1 = -1;
                        _selY1 = -1;
                        _selX2 = -1;
                        _selY2 = -1;
                        paintAll();
                        foreach (Cell item in gameServer)
                        {
                            if (item.Location.X == iX && item.Location.Y == iY)
                            {                                
                                if (item is Tower)
                                {
                                    _selX1 = iX;
                                    _selY1 = iY;
                                }
                                if (item is Unit)
                                {
                                    _selX1 = iX;
                                    _selY1 = iY;
                                }                                                           
                            }
                        }
                    }
                    else
                    {
                        foreach (Cell item in gameServer)
                        {
                            if (item.Location.X == iX && item.Location.Y == iY)
                            {
                                if (item is Tower)
                                {
                                    //это башня
                                }
                                if (item is Miner)
                                {
                                    //это miner
                                }
                                if (item is Producer)
                                {
                                    //это producer
                                }
                                if (item is Unit)
                                {
                                    //это unit
                                }
                            }
                        }

                        _selX2 = iX;
                        _selY2 = iY;
                        paintAll();
                    }
                }
            }
        }       
    }    
}
