using CODE_GameLib;
using System;
using System.Collections.Generic;

namespace CODE_Frontend
{
    public class GameView
    {
        private char[,] _board;

        public GameView()
        {
            Console.WriteLine("Please hit any keys or hit escape to exit...");
        }

        public void Draw(Game game)
        {
            Console.Clear();
            _board = new char[game.Player.CurrentRoom.Width,game.Player.CurrentRoom.Height];
            //TODO: draw Board

            bool color = false;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            for (int i = 0; i < game.Player.CurrentRoom.Height; i++)
            {
                for (int j = 0; j < game.Player.CurrentRoom.Width; j++)
                {
                    if (color)
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                        color = false;
                    }
                    else
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        color = true;
                    }
                    CalcChar(new Coordinate(j,i), game.Player);
                }
                Console.WriteLine();
            }

            Console.BackgroundColor = ConsoleColor.Black;
        }

        public void DrawEnd()
        {
            Console.WriteLine("Quitting game, goodbye!");
        }

        private void CalcChar(Coordinate coordinate, Player player)
        {
            var room = player.CurrentRoom;

            //Check if location is on the outside
            if (coordinate.X == 0 || coordinate.Y == 0 || coordinate.X == room.Width -1|| coordinate.Y == room.Height - 1)
            {
                _board[coordinate.X,coordinate.Y] = '#';
            }
            else if(coordinate.X == player.X && coordinate.Y == player.Y)
            {
                _board[coordinate.X, coordinate.Y] = 'X';
            }
            else
            {
                foreach (var item in room.Items)
                {
                    if (item.Coordinate.X == coordinate.X && item.Coordinate.Y == coordinate.Y)
                    {
                        //if (expr)
                        //{
                            
                        //}
                    }
                }
            }
        }
    }
}
