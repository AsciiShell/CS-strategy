﻿// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System.Drawing;
using System.Windows.Forms;

namespace GameLibrary
{
    public abstract class Building : Cell
    {
        public Building(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
        }
        public virtual void Produce() { }
        public override void Attack(Cell target) { }
        public override abstract void Draw(PaintEventArgs e, int sx, int sy, int otx, int oty);
    }

    public class Miner : Building
    {
        private const uint TIMER_TICK = 1500;
        private const uint MINER_COUNT = 1;
        private const uint MINER_HP = 200;
        public Miner(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
            Notifer.Subscribe(OnTimerTick, TIMER_TICK);
            HP = MINER_HP;
        }
        private bool OnTimerTick()
        {
            if (kind == Item.Kind.PAPER)
                Owner.ResourcesPaper += MINER_COUNT;
            else if (kind == Item.Kind.ROCK)
                Owner.ResourcesRock += MINER_COUNT;
            else if (kind == Item.Kind.SCISSORS)
                Owner.ResourcesScissors += MINER_COUNT;
            return IsAlive();
        }

        public override void Draw(PaintEventArgs e, int sx, int sy, int otx, int oty)
        {
            int sh = 8, vi = 8;
            Pen p = GetColor();
            System.Drawing.Point p1 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + vi * sy / 10);// первая точка
            System.Drawing.Point p2 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);// вторая точка
            System.Drawing.Point p3 = new System.Drawing.Point(Location.X * sx + otx + 2 * sx / 4, Location.Y * sy + oty + 1 * sy / 2);
            System.Drawing.Point p4 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);
            System.Drawing.Point p5 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + vi * sy / 10);

            e.Graphics.DrawLine(p, p1, p2);// рисуем линию
            e.Graphics.DrawLine(p, p3, p2);
            e.Graphics.DrawLine(p, p3, p4);
            e.Graphics.DrawLine(p, p5, p4);
            DrawKind(e, sx, sy, otx, oty);
        }
    }
    public class Tower : Building
    {
        private const uint TOWER_DAMAGE = 4;
        private const uint TOWER_HP = 500;
        private const uint TOWER_FREQ = 500;
        private bool TargetHandler()
        {
            if (Target is null || !Target.IsAlive() || !Target.Location.IsNearForLongRange(Location))
                foreach (Cell item in Owner.gameServer)
                {
                    if (item.Owner != Owner && item.IsAlive() && item.Location.IsNearForLongRange(Location))
                        Target = item;
                }
            if (!(Target is null))
                Target.GetDamage(this);
            return IsAlive();
        }
        public Tower(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
            HP = TOWER_HP;
            BaseDamage = TOWER_DAMAGE;
            Notifer.Subscribe(TargetHandler, TOWER_FREQ);
        }

        public override void Attack(Cell target)
        {
            if (target is null || !target.IsAlive() || !target.Location.IsNearForLongRange(Location))
                return;
            Target = target;
        }
        public override void Draw(PaintEventArgs e, int sx, int sy, int otx, int oty)
        {
            Pen p = GetColor();

            int sh = 8, vi = 8;

            System.Drawing.Point p1 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);// первая точка
            System.Drawing.Point p2 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);// вторая точка

            System.Drawing.Point p3 = new System.Drawing.Point(Location.X * sx + otx + sx / 2, Location.Y * sy + oty + (10 - vi) * sy / 10);
            System.Drawing.Point p4 = new System.Drawing.Point(Location.X * sx + otx + sx / 2, Location.Y * sy + oty + vi * sy / 10);

            e.Graphics.DrawLine(p, p1, p2);// рисуем линию
            e.Graphics.DrawLine(p, p3, p4);
            DrawKind(e, sx, sy, otx, oty);
        }
    }

    public class Producer : Building
    {
        private const uint TIMER_TICK = 100;
        private const uint PRODUCER_HP = 150;
        public const uint PRODUCER_PRICE = 10;
        private const uint PRODUCER_TICK = 1000 / TIMER_TICK;

        public uint UnitQueue { get; internal set; }
        public uint UnitProgress { get; internal set; }
        public Producer(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
            Notifer.Subscribe(OnTimerTick, TIMER_TICK);
            HP = PRODUCER_HP;
            UnitProgress = 0;
        }
        private bool OnTimerTick()
        {
            if (UnitQueue != 0)
            {
                UnitProgress++;
                if (UnitProgress == PRODUCER_TICK)
                {
                    new Unit(kind, new Point(Location), Owner);
                    UnitQueue--;
                    UnitProgress = 0;
                }
            }
            return IsAlive();
        }

        public override void Produce()
        {
            if (kind == Item.Kind.PAPER && Owner.ResourcesPaper >= PRODUCER_PRICE)
            {
                Owner.ResourcesPaper -= PRODUCER_PRICE;
                UnitQueue++;
            }
            else if (kind == Item.Kind.ROCK && Owner.ResourcesRock >= PRODUCER_PRICE)
            {
                Owner.ResourcesRock -= PRODUCER_PRICE;
                UnitQueue++;
            }
            else if (kind == Item.Kind.SCISSORS && Owner.ResourcesScissors >= PRODUCER_PRICE)
            {
                Owner.ResourcesScissors -= PRODUCER_PRICE;
                UnitQueue++;
            }
        }
        public override void Draw(PaintEventArgs e, int sx, int sy, int otx, int oty)
        {
            Pen p = GetColor();
            int sh = 7, vi = 8;

            System.Drawing.Point p1 = new System.Drawing.Point(Location.X * sx + otx + sx * (10 - sh) / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);// первая точка
            System.Drawing.Point p2 = new System.Drawing.Point(Location.X * sx + otx + sx * sh / 10, Location.Y * sy + oty + (10 - vi) * sy / 10);// вторая точка

            System.Drawing.Point p3 = new System.Drawing.Point(Location.X * sx + otx + (10 - sh) * sx / 10, Location.Y * sy + oty + vi * sy / 10);
            System.Drawing.Point p4 = new System.Drawing.Point(Location.X * sx + otx + sh * sx / 10, Location.Y * sy + oty + vi * sy / 10);

            e.Graphics.DrawLine(p, p3, p1);
            e.Graphics.DrawLine(p, p1, p2);// рисуем линию
            e.Graphics.DrawLine(p, p2, p4);
            DrawKind(e, sx, sy, otx, oty);
        }
    }
}
