// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using GameLibrary;
using System;
using System.Drawing;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            GameServer gameServer = new GameServer(new Player(Player.Kind.USER, "User1", Color.Red), new Player(Player.Kind.BOT, "BOT1", Color.Blue));
            while(gameServer.IsEnabled)
            {
                System.Threading.Thread.Sleep(10);

            }
        }
    }
}
