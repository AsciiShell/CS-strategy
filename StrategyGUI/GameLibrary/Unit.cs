// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
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
        public Unit(Item.Kind kind, Point location) : base(kind, location)
        {
            BaseDamage = UNIT_DAMAGE;
            HP = UNIT_HP;
        }

        public override void Attack(Cell target)
        {
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
    }
}
