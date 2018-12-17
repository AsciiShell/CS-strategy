// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Drawing;
using System.Windows.Forms;

namespace GameLibrary
{
    public class Unit : Cell
    {
        private const uint UNIT_DAMAGE = 10;
        private const uint UNIT_HP = 100;
        private const uint UNIT_FREQ = 1000;
        private bool IsEnabled = false;

        private bool TargetHandler()
        {
            if (Target.Location.IsNearForClose(Location))
                Target.GetDamage(this);
            else
                Move(Target.Location);
            IsEnabled = Target.IsAlive() && IsAlive();
            return IsEnabled;
        }
        public Unit(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
            BaseDamage = UNIT_DAMAGE;
            HP = UNIT_HP;
        }

        public override void Attack(Cell target)
        {
            if (target.Owner == Owner)
                return;
            Target = target;
            if (!IsEnabled)
                Notifer.Subscribe(TargetHandler, UNIT_FREQ);

        }
        public void Move(Point to)
        {
            if (Location != to)
            {
                if (Math.Abs(to.X - Location.X) > Math.Abs(to.Y - Location.Y))
                    Location.X += (to.X > Location.X) ? 1 : -1;
                else
                    Location.Y += (to.Y > Location.Y) ? 1 : -1;
            }
        }

        public override void Draw(PaintEventArgs e, int sx, int sy, int otx, int oty)
        {
            int sh = 8, vi = 8;
            Pen p = GetColor();
            System.Drawing.Point p1 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + vi * sy / 10);
            System.Drawing.Point p2 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);
            System.Drawing.Point p4 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);
            System.Drawing.Point p5 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + vi * sy / 10);

            e.Graphics.DrawLine(p, p1, p4);// рисуем линию
            e.Graphics.DrawLine(p, p2, p5);
            DrawKind(e, sx, sy, otx, oty);
        }
    }
}
