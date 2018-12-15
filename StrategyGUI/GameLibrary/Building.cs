// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
namespace GameLibrary
{
    public abstract class Building : Cell
    {
        public Building(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
        }
        public virtual uint Mine() => 0;
        public virtual void Produce() { }
        public virtual Unit GetUnit() => null;
        public override void Attack(Cell target) { }
    }

    public class Miner : Building
    {
        private const uint TIMER_TICK = 1000;
        private const uint MINER_COUNT = 1;
        private const uint MINER_HP = 200;
        public uint Storage { get; internal set; }

        public Miner(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
            Notifer.Subscribe(OnTimerTick, TIMER_TICK);
            Storage = 0;
            HP = MINER_HP;
        }
        private bool OnTimerTick()
        {
            Storage += MINER_COUNT;
            return IsAlive();
        }

        public override Unit GetUnit() { return null; }

        public override uint Mine()
        {
            var result = Storage;
            Storage = 0;
            return result;
        }

    }
    public class Tower : Building
    {
        private const uint TOWER_DAMAGE = 4;
        private const uint TOWER_HP = 500;
        private const uint TOWER_FREQ = 500;
        private bool TargetHandler()
        {
            if (Target.Location.IsNearForLongRange(Location))
                Target.GetDamage(this);
            return Target.IsAlive();
        }
        public Tower(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
            HP = TOWER_HP;
            BaseDamage = TOWER_DAMAGE;
        }

        public override void Attack(Cell target)
        {
            var lastTarget = Target;
            Target = target;
            if (lastTarget == null || !lastTarget.IsAlive())
                Notifer.Subscribe(TargetHandler, TOWER_FREQ);
        }

    }

    public class Producer : Building
    {
        private const uint TIMER_TICK = 100;
        private const uint PRODUCER_HP = 150;
        private const uint PRODUCER_PRICE = 10;
        private const uint PRODUCER_TICK = 1000 / TIMER_TICK;

        public uint UnitCount { get; internal set; }
        public uint UnitQueue { get; internal set; }
        public uint UnitProgress { get; internal set; }
        public uint Price() => PRODUCER_PRICE;
        public Producer(Item.Kind kind, Point location, Player player) : base(kind, location, player)
        {
            Notifer.Subscribe(OnTimerTick, TIMER_TICK);
            UnitCount = 0;
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
                    UnitCount++;
                    UnitQueue--;
                    UnitProgress = 0;
                }
            }
            return IsAlive();
        }
        public override Unit GetUnit()
        {
            if (UnitCount > 0)
            {
                UnitCount--;
                return new Unit(kind, Location, Owner);
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
