using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items;
using System;
using System.Linq;
using CODE_GameLib.Items.Adapter;
using CODE_GameLib.Services;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public Player Player;
        private readonly CheatService _cheatService;

        private const int AmountOfSankaraStonesInGame = 5;

        public Game(CheatService cheats)
        {
            _cheatService = cheats;
        }

        public void MovePlayer(Direction direction)
        {
            var targetCoordinate = CalcTargetCoordinate(direction, Player.Spot);
            var playerLives = Player.Lives;

            if (!CanPlayerMove(targetCoordinate)) return;

            if (IsCoordinateDoor(targetCoordinate))
            {
                var door = GetDoorOnLocation(targetCoordinate).ConnectsToDoor;

                if (_cheatService.WalkThroughDoors)
                {
                    targetCoordinate = MovePlayerThroughDoor(door);
                }
                else if (door.CanUseDoor(Player))
                {
                    targetCoordinate = MovePlayerThroughDoor(door);
                }
            }

            if (IsCoordinateIce(targetCoordinate))
            {
                targetCoordinate = MovePlayerOverIce(direction, targetCoordinate);
            }

            foreach (var item in Player.CurrentRoom.Items.Where(item => item.Coordinate.IsEqual(targetCoordinate)))
            {
                item.OnTouch(Player);
            }

            Player.Spot = targetCoordinate;

            MoveEnemies();

            if (_cheatService.LoseNoLives)
            {
                Player.Lives = playerLives;
            }

            Updated?.Invoke(this, this);
        }

        private void MoveEnemies()
        {
            foreach (var enemy in Player.CurrentRoom.Items.Where(item => item.GetType() == typeof(EnemyAdapter)).Cast<EnemyAdapter>())
            {
                enemy.Move();
                if (Player.Spot.IsEqual(enemy.Coordinate))
                {
                    enemy.OnTouch(Player);
                }
            }
        }

        private Coordinate MovePlayerThroughDoor(IDoor door)
        {
            door.UseDoor(Player);
            Player.CurrentRoom = door.IsInRoom;
            return door.Coordinate;
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

            return !IsCoordinateWall(targetCoordinate) && !IsCoordinateEnemy(targetCoordinate);
        }

        private Coordinate CalcTargetCoordinate(Direction direction, Coordinate playerPosition)
        {
            var x = playerPosition.X;
            var y = playerPosition.Y;
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

        private bool IsCoordinateEnemy(Coordinate targetCoordinate)
        {
            return Player.CurrentRoom.Items.Where(item => item.GetType() == typeof(EnemyAdapter)).Any(item => item.Coordinate.IsEqual(targetCoordinate));
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
                throw new ArgumentOutOfRangeException("The coordinate is not a door so this method cant return an IDoor.");

            return Player.CurrentRoom.Connections.FirstOrDefault(kvp => kvp.Key.X == coordinate.X && kvp.Key.Y == coordinate.Y).Value;
        }

        private bool IsCoordinateIce(Coordinate targetCoordinate) => Player.CurrentRoom.FloorTiles.Any(floorTile => targetCoordinate.IsEqual(floorTile.Coordinate));

        private Coordinate MovePlayerOverIce(Direction direction, Coordinate targetCoordinate)
        {
            while (CanPlayerMove(targetCoordinate) && IsCoordinateIce(targetCoordinate))
            {
                targetCoordinate = CalcTargetCoordinate(direction, targetCoordinate);
            }

            return targetCoordinate;
        }
    }
}
