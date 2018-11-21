// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using GameLib;
using System;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Point p = new Point(10, 10);
            Unit a = new Unit(Item.Kind.PAPER, p);
            Unit b = new Unit(Item.Kind.ROCK, p);
            a.Attack(b);
            p.X = 100;
            Console.WriteLine(a.Location.X);

        }
    }
}
