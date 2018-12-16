// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary
{
    public class Player
    {
        public enum Kind
        {
            USER,
            BOT
        }
        public List<Cell> Army { get; internal set; }
        public string Name { get; internal set; }
        public Kind Type { get; internal set; }
        public Player(Kind kind, string name)
        {
            Type = kind;
            Name = name;
            Army = new List<Cell>();
        }
        public void AddCell(Cell cell)
        {
            Army.Append(cell);
        }
        internal bool CleanMap()
        {
            bool someAlive = false;
            foreach (Cell item in Army)
            {
                if (item.IsAlive())
                    someAlive = true;
                if (item is Unit && !item.IsAlive())
                {
                    Army.Remove(item);
                }
            }
            return someAlive;
        }
    }
}
