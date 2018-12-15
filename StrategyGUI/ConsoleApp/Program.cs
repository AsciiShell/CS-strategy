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
            Cell a = new Unit(Item.Kind.SCISSORS, new Point(10, 0));
            Cell b = new Tower(Item.Kind.PAPER, new Point(10, 10));
            a.Attack(b);
            b.Attack(a);
            while(a.IsAlive() && b.IsAlive())
            {
                System.Threading.Thread.Sleep(10);

            }
        }
    }
}
