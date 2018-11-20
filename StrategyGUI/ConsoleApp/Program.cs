using GameLib;
using System;
namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Cell<Paper> a = new Cell<Paper>(); ;
            Console.WriteLine(a.type.paper_rate());
            Console.WriteLine(a.type.rock_rate());
            Console.WriteLine(a.type.scissors_rate());

        }
    }
}
