// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

using System.Drawing;

namespace GameLibrary
{
    public class Player
    {
        private const uint DEFAULT_RESOURCES = Producer.PRODUCER_PRICE;
        private const uint BOT_FREQ = 1000;
        public enum Kind
        {
            USER,
            BOT
        }
        public List<Cell> Army { get; internal set; }
        public string Name { get; internal set; }
        public Kind Type { get; internal set; }
        public uint ResourcesRock { get; internal set; }
        public uint ResourcesPaper { get; internal set; }
        public uint ResourcesScissors { get; internal set; }
        public bool SomeAlive { get; internal set; }
        public GameServer gameServer { get; internal set; }
        public Color Color { get; internal set; }
        public Player(Kind kind, string name, Color color)
        {
            Type = kind;
            Name = name;
            Army = new List<Cell>();
            SomeAlive = true;
            Color = color;
            ResourcesPaper = DEFAULT_RESOURCES;
            ResourcesRock = DEFAULT_RESOURCES;
            ResourcesScissors = DEFAULT_RESOURCES;
        }
        public void ConnectGame(GameServer game)
        {
            gameServer = game;
            if (Type == Kind.BOT)
                Notifer.Subscribe(AI, BOT_FREQ);
        }
        public void AddCell(Cell cell)
        {
            Army.Append(cell);
        }
        internal bool CleanMap()
        {
            SomeAlive = false;
            foreach (Cell item in Army)
            {
                if (item.IsAlive())
                    SomeAlive = true;
                if (item is Unit && !item.IsAlive())
                {
                    Army.Remove(item);
                }
            }
            return SomeAlive;
        }
        private bool AI()
        {
            Cell target = null;
            foreach (Cell item in gameServer)
            {
                if (item.Owner != this && item.IsAlive())
                {
                    target = item;
                }

            }
            if (!(target is null))
                foreach (Cell item in Army)
                {
                    if (item is Unit)
                        item.Attack(target);
                    if (item is Producer)
                        ((Building)item).Produce();
                }
            return SomeAlive;
        }
    }
}
