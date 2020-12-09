using CODE_GameLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace CODE_Frontend
{
    public class GameView
    {
        public GameView()
        {
            Console.WriteLine("Please hit any keys or hit escape to exit...");
        }

        public void Draw(ConsoleKey keyPressed)
        { 
            Console.WriteLine($"\nYou pressed {keyPressed}");
            //TODO: draw Board
        }

        public void DrawEnd()
        {
            Console.WriteLine("Quitting game, goodbye!");
        }
    }
}
