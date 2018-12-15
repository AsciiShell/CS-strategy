// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace GameLibrary
{
    public class GameServer
    {
        private const uint GAME_CLEAN_TIME = 1000;
        public List<Cell> Objects { get; internal set; }
        public Point Size { get; internal set; }
        public Player[] Players { get; internal set; }
        public bool IsEnabled { get; internal set; }
        public void SetMap1()
        {
            Size.X = 20;
            Size.Y = 9;
            Objects.Append(new Miner(Item.Kind.ROCK, new Point(0, 1), Players[0]));
            Objects.Append(new Miner(Item.Kind.SCISSORS, new Point(0, 4), Players[0]));
            Objects.Append(new Miner(Item.Kind.PAPER, new Point(0, 7), Players[0]));
            Objects.Append(new Producer(Item.Kind.ROCK, new Point(2, 2), Players[0]));
            Objects.Append(new Producer(Item.Kind.SCISSORS, new Point(2, 4), Players[0]));
            Objects.Append(new Producer(Item.Kind.PAPER, new Point(2, 6), Players[0]));
            Objects.Append(new Tower(Item.Kind.ROCK, new Point(4, 1), Players[0]));
            Objects.Append(new Tower(Item.Kind.SCISSORS, new Point(4, 4), Players[0]));
            Objects.Append(new Tower(Item.Kind.PAPER, new Point(4, 7), Players[0]));

            Objects.Append(new Miner(Item.Kind.ROCK, new Point(19, 1), Players[1]));
            Objects.Append(new Miner(Item.Kind.SCISSORS, new Point(19, 4), Players[1]));
            Objects.Append(new Miner(Item.Kind.PAPER, new Point(19, 7), Players[1]));
            Objects.Append(new Producer(Item.Kind.ROCK, new Point(17, 2), Players[1]));
            Objects.Append(new Producer(Item.Kind.SCISSORS, new Point(17, 4), Players[1]));
            Objects.Append(new Producer(Item.Kind.PAPER, new Point(17, 6), Players[1]));
            Objects.Append(new Tower(Item.Kind.ROCK, new Point(15, 1), Players[1]));
            Objects.Append(new Tower(Item.Kind.SCISSORS, new Point(15, 4), Players[1]));
            Objects.Append(new Tower(Item.Kind.PAPER, new Point(15, 7), Players[1]));
        }
        private bool CleanMap()
        {
            bool haveA = false, haveB = false;
            foreach (Cell item in Objects)
            {
                if (item.Owner == Players[0] && item.IsAlive())
                    haveA = true;
                else if (item.Owner == Players[1] && item.IsAlive())
                    haveB = true;
                if (item is Unit && !item.IsAlive())
                    Objects.Remove(item);
            }
            IsEnabled = haveA && haveB;
            return IsEnabled;
        }
        public GameServer(Player player1, Player player2)
        {
            Players = new Player[] { player1, player2 };
            Objects = new List<Cell>();
            Size = new Point(0, 0);
            IsEnabled = true;
            SetMap1();
            Notifer.Subscribe(CleanMap, GAME_CLEAN_TIME);
        }
    }
}
