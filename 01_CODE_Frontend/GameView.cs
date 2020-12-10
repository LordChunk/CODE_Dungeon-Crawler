using CODE_GameLib;
using System;
using System.Collections.Generic;
using CODE_GameLib.Doors;
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
                    _board[item.Coordinate.X, item.Coordinate.Y] = 'K';
                }
                else if(item.GetType() == typeof(PressurePlate))
                {
                    _board[item.Coordinate.X, item.Coordinate.Y] = 'T';
                }
                else if (item.GetType() == typeof(SankaraStone))
                {
                    _board[item.Coordinate.X, item.Coordinate.Y] = 'S';
                }
                else if (item.GetType() == typeof(SingleUseTrap))
                {
                    _board[item.Coordinate.X, item.Coordinate.Y] = '@';
                }
                else if (item.GetType() == typeof(Trap))
                {
                    _board[item.Coordinate.X, item.Coordinate.Y] = 'O';
                }
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
            foreach (var connection in player.CurrentRoom.Connections)
            {
                var coordinate = new Coordinate(0,0);

                switch (connection.Rooms)
                {
                    case Direction.North:
                        coordinate.X = (player.CurrentRoom.Width - 1) / 2;
                        coordinate.Y = 0;
                        _board[coordinate.X, coordinate.Y] = '=';
                        break;
                    case Direction.East:
                        coordinate.X = player.CurrentRoom.Width - 1;
                        coordinate.Y = (player.CurrentRoom.Height - 1) / 2;
                        _board[coordinate.X, coordinate.Y] = '|';
                        break;
                    case Direction.West:
                        coordinate.X = 0;
                        coordinate.Y = (player.CurrentRoom.Height - 1) / 2;
                        _board[coordinate.X, coordinate.Y] = '|';
                        break;
                    case Direction.South:
                        coordinate.X = (player.CurrentRoom.Width - 1) / 2;
                        coordinate.Y = player.CurrentRoom.Height - 1;
                        _board[coordinate.X, coordinate.Y] = '=';
                        break;
                }

                if (kvp.Value.GetType() == typeof(ToggleDoor))
                {
                    _board[coordinate.X, coordinate.Y] = '┴';
                }
                else if (kvp.Value.GetType() == typeof(SingleUseDoor))
                {
                    _board[coordinate.X, coordinate.Y] = '∩';
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
