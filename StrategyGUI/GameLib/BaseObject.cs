using System;

namespace GameLib
{
    public abstract class BaseObject
    {
        protected const float normal_rate = 1.0F;
        protected const float advantage_rate = 2.0F;
        protected const float disadvantage_rate = 1 / advantage_rate;

        public abstract float rock_rate();
        public abstract float paper_rate();
        public abstract float scissors_rate();

        public static implicit operator BaseObject(Type v)
        {
            switch (v.Name)
            {
                case "Rock":
                    return new Rock();
                case "Paper":
                    return new Paper();
                case "Scissors":
                    return new Scissors();
                default:
                    return null;

            }
        }
    }
    public class Rock : BaseObject
    {
        public override float paper_rate() => disadvantage_rate;

        public override float rock_rate() => normal_rate;

        public override float scissors_rate() => advantage_rate;
    }
    public class Paper : BaseObject
    {
        public override float paper_rate() => normal_rate;

        public override float rock_rate() => advantage_rate;

        public override float scissors_rate() => disadvantage_rate;
    }
    public class Scissors : BaseObject
    {
        public override float paper_rate() => advantage_rate;

        public override float rock_rate() => disadvantage_rate;

        public override float scissors_rate() => normal_rate;
    }
}
