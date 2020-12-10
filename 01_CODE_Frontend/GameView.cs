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
            foreach (var kvp in player.CurrentRoom.Connections)
            {
                var coordinate = new Coordinate(0,0);

                switch (kvp.Key)
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

                if (kvp.Value.GetType() == typeof(ToggleConnection))
                {
                    _board[coordinate.X, coordinate.Y] = '┴';
                }
                else if (kvp.Value.GetType() == typeof(SingleUseConnection))
                {
                    _board[coordinate.X, coordinate.Y] = '∩';
                }
            }
        }

        private void DrawBoard(Player player)
        {
            Console.Clear();

            bool color = false;
            Console.BackgroundColor = ConsoleColor.DarkGray;

            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
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

                    Console.ForegroundColor = DetermineColor(_board[j, i]);
                    Console.Write(_board[j,i]);
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private ConsoleColor DetermineColor(char c)
        {
            if (c == '#')
            {
                return ConsoleColor.Yellow;
            }
            else if (c == '@')
            {
                return ConsoleColor.Black;
            }
            else if(c == 's')
            {
                return ConsoleColor.DarkRed;
            }
            else if (c == 'O')
            {
                return ConsoleColor.Red;
            }
            else if (c == 'X')
            {
                return ConsoleColor.Cyan;
            }
            else if (c == '=' || c == '|')
            {
                return ConsoleColor.DarkGreen;
            }
            else if (c == '∩')
            {
                return ConsoleColor.DarkBlue;
            }
            else if (c == '┴')
            {
                return ConsoleColor.DarkBlue;
            }
            else 
            {
                return ConsoleColor.DarkBlue;
            }
        }
    }
}
