using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using System;
using System.Linq;
using CODE_GameLib.Tiles;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public Player Player;

        public int AmountOfSankaraStonesInGame = 5;

        public void MovePlayer(Direction direction)
        {
            var targetCoordinate = CalcTargetCoordinate(direction, Player.Spot, Player.CurrentRoom);
            // Check if move if valid
            if (!CanPlayerMove(targetCoordinate)) return;

            // Move enemies
            Player.CurrentRoom.Enemies.ForEach(e => e.Move());

            if (IsCoordinateDoor(targetCoordinate))
            {
                var door = GetDoorOnLocation(targetCoordinate);

                if (door.UseDoor(Player))
                {
                    Updated?.Invoke(this, this);
                    return;
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

            // Check for additional conveyor belt moves
            ConveyorBelt.MoveEntities(Player);

            Updated?.Invoke(this, this);
        }

        public bool DidPlayerWin()
        {
            return Player.Items.Count(item => item.GetType() == typeof(SankaraStone)) >= AmountOfSankaraStonesInGame;
        }

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

        public static Coordinate CalcTargetCoordinate(Direction direction, Coordinate currentCoordinate, Room room)
        {
            var x = currentCoordinate.X;
            var y = currentCoordinate.Y;
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
                    if (x > room.Width - 1)
                    {
                        x = room.Width - 1;
                    }
                    break;
                case Direction.South:
                    y++;
                    if (y > room.Height - 1)
                    {
                        y = room.Height - 1;
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
            if (Player.CurrentRoom.Height - 1 != targetCoordinate.Y &&
                Player.CurrentRoom.Width - 1 != targetCoordinate.X && targetCoordinate.Y != 0 &&
                targetCoordinate.X != 0) return false;
            return !IsCoordinateDoor(targetCoordinate);
        }

        private bool IsCoordinateDoor(Coordinate targetCoordinate)
        {
            var doorLocation = Player.CurrentRoom.Connections.Select(kvp => kvp.Key).ToList();
            doorLocation = doorLocation.Where(coordinate => coordinate.IsEqual(targetCoordinate)).ToList();
            return doorLocation.FirstOrDefault() != null;
        }

        private IDoor GetDoorOnLocation(Coordinate coordinate)
        {
            if (!IsCoordinateDoor(coordinate))
                throw new ArgumentOutOfRangeException(nameof(coordinate), "The coordinate is not a door so this method cant return an IDoor.");

            return Player.CurrentRoom.Connections.FirstOrDefault(kvp => kvp.Key.X == coordinate.X && kvp.Key.Y == coordinate.Y).Value;
        }
    }
}
