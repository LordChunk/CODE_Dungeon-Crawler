using CODE_GameLib;
using CODE_GameLib.Doors;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using System;
using CODE_GameLib.FloorTiles;
using CODE_GameLib.Items.Adapter;

namespace CODE_Frontend
{
    public class GameView
    {
        private CharWithColor[,] _board;

        public GameView()
        {
            Console.WriteLine("Please hit any keys or hit escape to exit...");
        }

        public void Draw(Game game)
        {
            Console.Clear();

            if (game.DidPlayerWin())
            {
                DrawWin();
            }
            else if (game.IsPlayerDead())
            {
                DrawLose();
            }
            else
            {
                _board = new CharWithColor[game.Player.CurrentRoom.Width, game.Player.CurrentRoom.Height];
                CalcBoard(game.Player);
                DrawBoard();
            }
        }

        public void DrawEnd()
        {
            Console.WriteLine("Quitting game, goodbye!");
        }

        public void DrawWin()
        {
            Console.WriteLine("You did it. You crazy son of a bitch, you did it! You picked up all the stones!");
            Console.WriteLine("Press esc to quit.");
        }

        public void DrawLose()
        {
            Console.WriteLine("You absolute fucktard you died!");
            Console.WriteLine("Press esc to quit.");
        }

        private void CalcBoard(Player player)
        {
            //put all item positions in array
            CalcItem(player);

            //put all wall positions in array
            CalcWalls(player);

            //put all door positions in array
            CalcDoors(player);

            //put all floor tile positions in array
            CalcFloorTiles(player);

            //put player position in array
            CalcPlayer(player);
        }

        private void CalcPlayer(Player player)
        {
            _board[player.Spot.X, player.Spot.Y] = new CharWithColor('X', ConsoleColor.Yellow);
        }

        private void CalcItem(Player player)
        {
            foreach (var item in player.CurrentRoom.Items)
            {
                if (item.GetType() == typeof(Key))
                {
                    var temp = (Key)item;
                    if (!temp.IsPickedUp)
                    {
                        _board[item.Coordinate.X, item.Coordinate.Y] = new CharWithColor('K', (ConsoleColor)Enum.Parse(typeof(ConsoleColor), temp.ColorCode.Name));
                    }
                }
                else if (item.GetType() == typeof(PressurePlate))
                {
                    _board[item.Coordinate.X, item.Coordinate.Y] = new CharWithColor('T', ConsoleColor.Yellow);
                }
                else if (item.GetType() == typeof(SankaraStone))
                {
                    var temp = (SankaraStone)item;
                    if (!temp.IsPickedUp)
                    {
                        _board[item.Coordinate.X, item.Coordinate.Y] = new CharWithColor('S', ConsoleColor.Yellow);
                    }
                }
                else if (item.GetType() == typeof(SingleUseTrap))
                {
                    var temp = (SingleUseTrap)item;
                    if (!temp.IsUsed)
                    {
                        _board[item.Coordinate.X, item.Coordinate.Y] = new CharWithColor('@', ConsoleColor.Yellow);
                    }
                }
                else if (item.GetType() == typeof(Trap))
                {
                    _board[item.Coordinate.X, item.Coordinate.Y] = new CharWithColor('O', ConsoleColor.Yellow);
                }
                else if (item.GetType() == typeof(EnemyAdapter))
                {
                    _board[item.Coordinate.X, item.Coordinate.Y] = new CharWithColor('E', ConsoleColor.DarkRed);
                }
            }
        }

        private void CalcDoors(Player player)
        {
            foreach (var kvp in player.CurrentRoom.Connections)
            {
                //calculate location door
                var coordinate = kvp.Key;
                AddDoor(kvp.Key, kvp.Value);

                //check type door
                if (kvp.Value.GetType() == typeof(ToggleDoor))
                {
                    _board[coordinate.X, coordinate.Y] = new CharWithColor('┴', ConsoleColor.Yellow);
                }
                else if (kvp.Value.GetType() == typeof(SingleUseDoor))
                {
                    _board[coordinate.X, coordinate.Y] = new CharWithColor('∩', ConsoleColor.Yellow);
                }
                else if (kvp.Value.GetType() == typeof(ColorCodedDoor))
                {
                    var temp = (ColorCodedDoor)kvp.Value;
                    if (coordinate.X == 0 || coordinate.X == kvp.Value.IsInRoom.Width)
                    {
                        _board[coordinate.X, coordinate.Y] = new CharWithColor('|',
                            (ConsoleColor)Enum.Parse(typeof(ConsoleColor), temp.ColorCode.Name));
                    }
                    else
                    {
                        _board[coordinate.X, coordinate.Y] = new CharWithColor('=',
                        (ConsoleColor)Enum.Parse(typeof(ConsoleColor), temp.ColorCode.Name));
                    }
                }
                else if (kvp.Value.GetType() == typeof(Ladder))
                {
                    _board[coordinate.X, coordinate.Y] = new CharWithColor('Ξ', ConsoleColor.Yellow);
                }
                else
                {
                    _board[coordinate.X, coordinate.Y] = new CharWithColor(' ', ConsoleColor.Black);
                }
            }
        }

        private void CalcWalls(Player player)
        {
            for (var i = 0; i < player.CurrentRoom.Width; i++)
            {
                _board[i, 0] = new CharWithColor('#', ConsoleColor.Yellow);
                _board[i, player.CurrentRoom.Height - 1] = new CharWithColor('#', ConsoleColor.Yellow);
            }

            for (var i = 0; i < player.CurrentRoom.Height; i++)
            {
                _board[0, i] = new CharWithColor('#', ConsoleColor.Yellow);
                _board[player.CurrentRoom.Width - 1, i] = new CharWithColor('#', ConsoleColor.Yellow);
            }
        }

        private void CalcFloorTiles(Player player)
        {
            foreach (var floorTile in player.CurrentRoom.FloorTiles)
            {
                if (floorTile.GetType() == typeof(IceFloorTile))
                {
                    _board[floorTile.Coordinate.X, floorTile.Coordinate.Y] = new CharWithColor('~', ConsoleColor.Yellow);
                }
            }
        }

        private void AddDoor(Coordinate coordinate, IDoor door)
        {
            if (door.GetType() == typeof(ColorCodedDoor))
            {
                var temp = (ColorCodedDoor)door;
                _board[coordinate.X, coordinate.Y] = new CharWithColor('=',
                    (ConsoleColor)Enum.Parse(typeof(ConsoleColor), temp.ColorCode.Name));
                _board[coordinate.X, coordinate.Y] = new CharWithColor('=', ConsoleColor.Yellow);
            }
            else
            {
                _board[coordinate.X, coordinate.Y] = new CharWithColor(' ', ConsoleColor.Black);
            }
        }

        private void DrawBoard()
        {
            //bool color = false;
            //Console.BackgroundColor = ConsoleColor.DarkGray;

            for (var i = 0; i < _board.GetLength(1); i++)
            {
                for (var j = 0; j < _board.GetLength(0); j++)
                {
                    if (_board[j, i] != null)
                    {
                        Console.ForegroundColor = _board[j, i].Color;
                        Console.Write(_board[j, i].C);
                    }
                    else
                    {
                        Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
