using System;

namespace GameLib
{
    public class Cell<T> where T : BaseObject
    {
        public BaseObject type;

        public Cell()
        {
            this.type = typeof(T);
        }
    }

}
