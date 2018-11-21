// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System.Timers;
namespace GameLibrary
{
    public abstract class Building : Cell
    {
        public Building(Item.Kind kind, Point location) : base(kind, location)
        {
        }
        public virtual uint Mine() => 0;
        public abstract void Produce();
        public abstract Unit GetUnit();
    }

    public class Miner : Building
    {
        private const uint TIMER_TICK = 1000;
        private const uint MINER_COUNT = 1;
        private const uint MINER_HP = 200;
        private Timer timer;
        public uint Storage { get; internal set; }

        public Miner(Item.Kind kind, Point location) : base(kind, location)
        {
            timer = new Timer(TIMER_TICK);
            timer.Elapsed += OnTimerTick;
            timer.AutoReset = true;
            timer.Start();
            Storage = 0;
            HP = MINER_HP;
        }
        private void OnTimerTick(object src, ElapsedEventArgs args)
        {
            Storage += MINER_COUNT;
        }
        public override void Attack(Cell target) { }

        public override Unit GetUnit() { return null; }

        public override uint Mine()
        {
            var result = Storage;
            Storage = 0;
            return result;
        }

        public override void Produce() { }
    }
    public class Tower : Building
    {
        private const uint TOWER_DAMAGE = 4;
        private const uint TOWER_HP = 500;

        public Tower(Item.Kind kind, Point location) : base(kind, location)
        {
            HP = TOWER_HP;
            BaseDamage = TOWER_DAMAGE;
        }

        public override void Attack(Cell target)
        {
            if (target.Location.IsNearForLongRange(Location))
                target.GetDamage(this);
        }

        public override Unit GetUnit() => null;

        public override void Produce() { }
    }

    public class Producer : Building
    {
        private const uint TIMER_TICK = 1000;
        private const uint TIMER_WAIT_TICK = 50;
        private const uint PRODUCER_HP = 150;
        private const uint PRODUCER_PRICE = 10;
        private Timer timer;
        public uint UnitCount { get; internal set; }
        public uint UnitQueue { get; internal set; }
        public uint Price() => PRODUCER_PRICE;
        public Producer(Item.Kind kind, Point location) : base(kind, location)
        {
            timer = new Timer(TIMER_WAIT_TICK);
            timer.Elapsed += OnTimerTick;
            timer.AutoReset = true;
            timer.Start();
            UnitCount = 0;
            HP = PRODUCER_HP;
        }
        private void OnTimerTick(object src, ElapsedEventArgs args)
        {
            if (UnitQueue != 0)
            {
                if (timer.Interval == TIMER_TICK)
                {
                    UnitCount++;
                    UnitQueue--;
                }
                else
                    timer.Interval = TIMER_TICK;
            }
            else
                timer.Interval = TIMER_WAIT_TICK;
        }
        public override void Attack(Cell target) { }
        public override Unit GetUnit()
        {
            if (UnitCount > 0)
            {
                UnitCount--;
                return new Unit(kind, Location);
            }
            else
                return null;
        }

        public override void Produce()
        {
            UnitQueue++;
        }
    }
}
