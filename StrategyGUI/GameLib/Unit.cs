// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
namespace GameLib
{
    public class Unit : Cell
    {
        public Unit(Item.Kind kind, Point location) : base(kind, location)
        {
            BaseDamage = 10;
        }

        public override void Attack(Cell target)
        {
            if (target.Location.IsNearForClose(Location))
                target.GetDamage(this);
            else
                Move(target.Location);
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
    }
}
