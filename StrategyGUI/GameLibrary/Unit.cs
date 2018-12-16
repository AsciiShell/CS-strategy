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
        private const uint UNIT_FREQ = 700;

        private bool TargetHandler()
        {
            if (Target.Location.IsNearForClose(Location))
                Target.GetDamage(this);
            else
                Move(Target.Location);
            return Target.IsAlive();
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
            var lastTarget = Target;
            Target = target;
            if (lastTarget == null || !lastTarget.IsAlive())
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
            Pen p = new Pen(Color.Red, 2);
            System.Drawing.Point p1 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + vi * sy / 10);// первая точка
            System.Drawing.Point p2 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);// вторая точка
            System.Drawing.Point p3 = new System.Drawing.Point(Location.X * sx + otx + 2 * sx / 4, Location.Y * sy + oty + 1 * sy / 2);
            System.Drawing.Point p4 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);
            System.Drawing.Point p5 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + vi * sy / 10);

            e.Graphics.DrawLine(p, p1, p2);// рисуем линию
            e.Graphics.DrawLine(p, p3, p2);
            e.Graphics.DrawLine(p, p3, p4);
            e.Graphics.DrawLine(p, p5, p4);
        }
    }
}
