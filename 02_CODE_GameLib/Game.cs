using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public Player Player;

        private const int AmountOfSankaraStonesInGame = 5;

        public void MovePlayer(Direction direction)
        {
            // Move enemies
            Player.CurrentRoom.Enemies.ForEach(e => e.Move());

            var targetCoordinate = CalcTargetCoordinate(direction);
            // Check if move if valid
            if (!CanPlayerMove(targetCoordinate)) return;

            if (IsCoordinateDoor(targetCoordinate))
            {
                var door = GetDoorOnLocation(targetCoordinate).ConnectsToDoor;

                if (door.CanUseDoor(Player))
                {
                    door.UseDoor(Player);
                    Player.UpdateCurrentRoom(door.IsInRoom);
                    targetCoordinate = door.Coordinate;
                }
            }

            // Pickup items
            foreach (var item in Player.CurrentRoom.Items.Where(item => item.Coordinate.IsEqual(targetCoordinate)))
            {
                item.OnTouch(Player);
            }

            // Move player
            Player.Spot = targetCoordinate;

            // Check enemy collisions
            Player.CurrentRoom.Enemies.ForEach(enemy =>
            {
                if (enemy.CurrentXLocation == Player.Spot.X && enemy.CurrentYLocation == Player.Spot.Y)
                    Player.Lives--;
            });

            Updated?.Invoke(this, this);
        }

        public bool DidPlayerWin() =>
            Player.Items.Count(item => item.GetType() == typeof(SankaraStone)) >= AmountOfSankaraStonesInGame;

        public bool IsPlayerDead() =>
            Player.Lives <= 0;

        private bool CanPlayerMove(Coordinate targetCoordinate)
        {
            if (DidPlayerWin() || IsPlayerDead())
            {
                return false;
            }

            return !IsCoordinateWall(targetCoordinate);
        }

        private Coordinate CalcTargetCoordinate(Direction direction)
        {
            var x = Player.Spot.X;
            var y = Player.Spot.Y;
            switch (direction)
            {
                case Direction.North:
                    y--;
                    if (y < 0)
                    {
                        y = 0;
                    }

                    break;
                case Direction.East:
                    x++;
                    if (x > Player.CurrentRoom.Width - 1)
                    {
                        x = Player.CurrentRoom.Width - 1;
                    }
                    break;
                case Direction.South:
                    y++;
                    if (y > Player.CurrentRoom.Height - 1)
                    {
                        y = Player.CurrentRoom.Height - 1;
                    }
                    break;
                case Direction.West:
                    x--;
                    if (x < 0)
                    {
                        x = 0;
                    }
                    break;
            }
            return new Coordinate(x, y);
        }

        private bool IsCoordinateWall(Coordinate targetCoordinate)
        {
            if (Player.CurrentRoom.Height - 1 == targetCoordinate.Y || Player.CurrentRoom.Width - 1 == targetCoordinate.X || targetCoordinate.Y == 0 || targetCoordinate.X == 0)
            {
                if (!IsCoordinateDoor(targetCoordinate))
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsCoordinateDoor(Coordinate targetCoordinate)
        {
            var doorLocation = new List<Coordinate>();

            foreach (var kvp in Player.CurrentRoom.Connections)
            {
                doorLocation.Add(kvp.Key);
            }

            doorLocation = doorLocation.Where(coordinate => coordinate.IsEqual(targetCoordinate)).ToList();
            return doorLocation.FirstOrDefault() != null;
        }

        private Coordinate CalcDoorLocation(Direction direction)
        {
            return direction switch
            {
                Direction.North => new Coordinate((Player.CurrentRoom.Width - 1) / 2, 0),
                Direction.East => new Coordinate(Player.CurrentRoom.Width - 1, (Player.CurrentRoom.Height - 1) / 2),
                Direction.South => new Coordinate((Player.CurrentRoom.Width - 1) / 2, Player.CurrentRoom.Height - 1),
                Direction.West => new Coordinate(0, (Player.CurrentRoom.Height - 1) / 2),
                _ => throw new ArgumentOutOfRangeException(nameof(direction),"The direction you gave as input was not found. Did you add a new direction to the Direction enum?")
            };
        }

        private IDoor GetDoorOnLocation(Coordinate coordinate)
        {
            if (!IsCoordinateDoor(coordinate))
                throw new ArgumentOutOfRangeException(nameof(coordinate),"The coordinate is not a door so this method cant return an IDoor.");

            return Player.CurrentRoom.Connections.FirstOrDefault(kvp => kvp.Key.X == coordinate.X && kvp.Key.Y == coordinate.Y).Value; 
        }

        private Coordinate CalcNewLocation(Direction direction)
        {
            return direction switch
            {
                Direction.North => new Coordinate((Player.CurrentRoom.Width - 1) / 2, 0),
                Direction.East => new Coordinate(Player.CurrentRoom.Width - 1, (Player.CurrentRoom.Height - 1) / 2),
                Direction.South => new Coordinate((Player.CurrentRoom.Width - 1) / 2, Player.CurrentRoom.Height - 1),
                Direction.West => new Coordinate(0, (Player.CurrentRoom.Height - 1) / 2),
                _ => throw new ArgumentOutOfRangeException(nameof(direction),"The direction you gave as input was not found. Did you add a new direction to the Direction enum?")
            };
        }
    }
}
