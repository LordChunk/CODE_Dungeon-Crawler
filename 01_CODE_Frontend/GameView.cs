using CODE_GameLib;
using System;

namespace CODE_Frontend
{
    public class GameView
    {
        public GameView()
        {
            Console.WriteLine("Please hit any keys or hit escape to exit...");
        }

        public void Draw(Game game)
        {
            Console.Clear();
            //TODO: draw Board

            bool color = false;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            for (int i = 0; i < game.Player.CurrentRoom.Height; i++)
            {
                for (int j = 0; j < game.Player.CurrentRoom.Width; j++)
                {
                    if (color)
                    {
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        color = false;
                    }
                    else
                    {
                        Console.Write(" ");
                        Console.BackgroundColor = ConsoleColor.White;
                        color = true;
                    }
                }
                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DrawEnd()
        {
            Console.WriteLine("Quitting game, goodbye!");
        }
    }
}
