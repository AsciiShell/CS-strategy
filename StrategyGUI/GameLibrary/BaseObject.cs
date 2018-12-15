// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com

namespace GameLibrary
{
    public abstract class Item
    {
        // Consts are always static
        // https://stackoverflow.com/questions/13150343/the-constant-cannot-be-marked-static
        private const float normal_rate = 1.0F;
        private const float advantage_rate = 2.0F;
        private const float disadvantage_rate = 1 / advantage_rate;
        public enum Kind
        {
            ROCK,
            PAPER,
            SCISSORS
        }
        public static float GetRate(Kind a, Kind b)
        {
            if (a == b)
                return normal_rate;
            else if (a == Kind.ROCK)
            {
                if (b == Kind.SCISSORS)
                    return advantage_rate;
                else return disadvantage_rate;
            }
            else if (a == Kind.PAPER)
            {
                if (b == Kind.ROCK)
                    return advantage_rate;
                else return disadvantage_rate;
            }
            else if (b == Kind.PAPER)
                return advantage_rate;
            else return disadvantage_rate;
        }
    }

}
