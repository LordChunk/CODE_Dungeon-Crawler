using CODE_GameLib;
using System;
using System.Collections.Generic;
using CODE_GameLib.Enums;
using CODE_GameLib.Items;

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
            _board = new char[game.Player.CurrentRoom.Width, game.Player.CurrentRoom.Height];
            CalcBoard(game.Player);
            DrawBoard(game.Player);
        }

        public void DrawEnd()
        {
            Console.WriteLine("Quitting game, goodbye!");
        }

        private void CalcBoard(Player player)
        {
            //put player position in array
            _board[player.X, player.Y] = 'X';

            //put all item position in array
            foreach (var item in player.CurrentRoom.Items)
            {
                if (item.GetType() == typeof(Key))
                {
                    
                }
                
                //_board[item.Coordinate.X, item.Coordinate.Y] = (char)item.Type;
            }

            //put all walls position in array
            for (int i = 0; i < player.CurrentRoom.Width; i++)
            {
                _board[i, 0] = '#';
                _board[i, player.CurrentRoom.Height - 1] = '#';
            }
            for (int i = 0; i < player.CurrentRoom.Height; i++)
            {
                _board[0, i] = '#';
                _board[player.CurrentRoom.Width - 1, i] = '#';
            }

            //put all door position in array
            foreach (var kvp in player.CurrentRoom.Doors)
            {
                switch (kvp.Key)
                {
                    case Direction.North:
                        _board[(player.CurrentRoom.Width - 1) / 2, 0] = '=';
                        break;
                    case Direction.East:
                        _board[player.CurrentRoom.Width - 1, (player.CurrentRoom.Width - 1) / 2] = '|';
                        break;
                    case Direction.West:
                        _board[0, (player.CurrentRoom.Height - 1) / 2] = '|';
                        break;
                    case Direction.South:
                        _board[(player.CurrentRoom.Width - 1) / 2, player.CurrentRoom.Height - 1] = '=';
                        break;
                }
            }

            //var room = player.CurrentRoom;

            ////Check if location is on the outside
            //if (coordinate.X == 0 || coordinate.Y == 0 || coordinate.X == room.Width -1|| coordinate.Y == room.Height - 1)
            //{
            //    _board[coordinate.X,coordinate.Y] = '#';
            //}
            //else if(coordinate.X == player.X && coordinate.Y == player.Y)
            //{
            //    _board[coordinate.X, coordinate.Y] = 'X';
            //}
            //else
            //{
            //    foreach (var item in room.Items)
            //    {
            //        if (item.Coordinate.X == coordinate.X && item.Coordinate.Y == coordinate.Y)
            //        {
            //            _board[coordinate.X, coordinate.Y] = (char)item.Type;
            //            break;
            //        }
            //    }
            //}
        }

        private void DrawBoard(Player player)
        {
            //Console.Clear();
            //_board = new char[game.Player.CurrentRoom.Width, game.Player.CurrentRoom.Height];
            ////TODO: draw Board

            //bool color = false;
            //for (int i = 0; i < game.Player.CurrentRoom.Height; i++)
            //{
            //    for (int j = 0; j < game.Player.CurrentRoom.Width; j++)
            //    {
            //        if (color)
            //        {
            //            Console.BackgroundColor = ConsoleColor.DarkGray;
            //            color = false;
            //        }
            //        else
            //        {
            //            Console.BackgroundColor = ConsoleColor.White;
            //            color = true;
            //        }
            //        CalcChar(new Coordinate(j, i), game.Player);
            //    }
            //    Console.WriteLine();
            //}
            //Console.BackgroundColor = ConsoleColor.DarkGray;



            //Console.BackgroundColor = ConsoleColor.Black;
        }
    }
}
