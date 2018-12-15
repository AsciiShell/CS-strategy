// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using GameLibrary;
using System;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Cell> list = new List<Cell>();
            list.Append(new Producer(Item.Kind.PAPER, new Point(10, 10)));
            list.Append(new Producer(Item.Kind.PAPER, new Point(20, 20)));
            list.Append(new Producer(Item.Kind.PAPER, new Point(30, 30)));
            list.Append(new Producer(Item.Kind.PAPER, new Point(40, 40)));
            while (list.Count != 0)
            {
                Cell last = null;
                foreach (Cell item in list)
                {
                    Console.WriteLine(item.Location);
                    if (last == null)
                        last = item;
                }
                list.Remove(last);
                Console.WriteLine();
            }

        }
    }
}
