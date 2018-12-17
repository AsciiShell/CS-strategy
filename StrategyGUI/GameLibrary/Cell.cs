// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameLibrary
{
    public class Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point point)
        {
            X = point.X;
            Y = point.Y;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Point))
            {
                return false;
            }

            var point = (Point)obj;
            return X == point.X &&
                   Y == point.Y;
        }

        public double GetDistance(Point p)
        {
            return Math.Sqrt(Math.Pow(X - p.X, 2) + Math.Pow(Y - p.Y, 2));
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }

        public bool IsNearForClose(Point p)
        {
            return GetDistance(p) < 1.5;
        }
        public bool IsNearForLongRange(Point p)
        {
            return GetDistance(p) <= 4;
        }
        public static bool operator ==(Point a, Point b)
        {
            if (a is null || b is null)
                return false;
            else
                return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Point a, Point b)
        {
            return !(a == b);
        }
        public override string ToString() => X.ToString() + " " + Y.ToString();
    }
    public abstract class Cell
    {
        public uint BaseDamage { get; internal set; }
        public Item.Kind kind;
        public Point Location { get; internal set; }
        public uint HP { get; internal set; }
        public Player Owner { get; internal set; }
        protected Cell Target = null;
        public Cell(Item.Kind kind, Point location, Player owner)
        {
            this.kind = kind;
            Location = location;
            BaseDamage = 0;
            Owner = owner;
            Owner.AddCell(this);
        }

        public void GetDamage(Cell sender)
        {
            float damage = Item.GetRate(sender.kind, kind) * sender.BaseDamage;
            if (damage > HP)
                HP = 0;
            else
                HP -= (uint)damage;
            Console.WriteLine(HP);
        }
        protected Pen GetColor() => IsAlive() ? new Pen(Owner.Color, 2) : new Pen(Color.Gray, 2);
        public abstract void Attack(Cell target);
        public bool IsAlive() => HP != 0;
        public abstract void Draw(PaintEventArgs e, int sx, int sy, int otx, int oty);

        protected void DrawKind(PaintEventArgs e, int sx, int sy, int otx, int oty)
        {
            int sh = 8, vi = 8;
            Pen p = GetColor();
            System.Drawing.Point p1 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + vi * sy / 10);// первая точка
            System.Drawing.Point p2 = new System.Drawing.Point(Location.X * sx + otx + sx * (sh + 3) / 10, Location.Y * sy + oty + vi * sy / 10);// вторая точка
            System.Drawing.Point p3 = new System.Drawing.Point(Location.X * sx + otx + (sh + 3) * sx / 10, Location.Y * sy + oty + (vi + 3) * sy / 10);
            System.Drawing.Point p4 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + (3 + vi) * sy / 10);
            System.Drawing.Point p5 = new System.Drawing.Point((p1.X + p3.X) / 2, (p1.Y + p3.Y) / 2);


            if (kind == Item.Kind.PAPER)
            {
                e.Graphics.DrawLine(p, p1, p2);
                e.Graphics.DrawLine(p, p2, p3);
                e.Graphics.DrawLine(p, p3, p4);
                e.Graphics.DrawLine(p, p4, p1);
            }
            else if (kind == Item.Kind.ROCK)
            {
                e.Graphics.DrawEllipse(p, p1.X, p1.Y, p3.X - p1.X, p3.Y - p1.Y);
            }
            else
            {
                e.Graphics.DrawLine(p, p1, p3);
                e.Graphics.DrawLine(p, p2, p4);

            }
        }
    }
}
