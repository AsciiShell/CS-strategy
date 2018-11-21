// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace GameLib
{
    public struct Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public abstract class Cell
    {
        public uint baseDamage { get; internal set; }
        public Item.Kind kind;
        public Point Location { get; internal set; }
        public uint HP { get; internal set; }
        protected Cell(Item.Kind kind, Point location)
        {
            this.kind = kind;
            Location = location;
            baseDamage = 10;
        }

        public void GetDamage(Cell sender)
        {
            float damage = Item.GetRate(sender.kind, kind) * sender.baseDamage;
            if (damage > HP)
                HP = 0;
            else
                HP -= (uint)damage;
        }
        public abstract void Attack(Cell target);
        public bool IsAlive() => HP == 0;
    }
    public class Unit : Cell
    {
        public Unit(Item.Kind kind, Point location) : base(kind, location)
        {

        }

        public override void Attack(Cell target)
        {
            // CHECK distanse
            target.GetDamage(this);
        }
    }
}
