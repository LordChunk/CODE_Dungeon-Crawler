using System;
using System.Linq;
using CODE_GameLib.Enums;
using CODE_GameLib.Items;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public Player Player;

        private const int AmountOfSankaraStonesInGame = 5;

        public void MovePlayer(Direction direction)
        {
            Coordinate targetCoordinate = CalcTargetCoordinate(direction);
            if (CanPlayerMove(targetCoordinate))
            {
                foreach (var item in Player.CurrentRoom.Items)
                {
                    if (item.Coordinate.IsEqual(targetCoordinate))
                    {
                        item.OnTouch(Player);
                    }
                }

                Player.Spot = targetCoordinate;

                Updated?.Invoke(this, this);
            }
        }

        public bool DidPlayerWin() => 
            Player.Items.Count(item => item.GetType() == typeof(SankaraStone)) == AmountOfSankaraStonesInGame;

        private bool CanPlayerMove(Coordinate targetCoordinate)
        {
            if (IsCoordinateWall(targetCoordinate))
            {
                return false;
            }

            return true;
        }

        private Coordinate CalcTargetCoordinate(Direction direction)
        {
            return direction switch
            {
                Direction.North => new Coordinate(Player.Spot.X, Player.Spot.Y - 1),
                Direction.East => new Coordinate(Player.Spot.X + 1, Player.Spot.Y),
                Direction.South => new Coordinate(Player.Spot.X, Player.Spot.Y + 1),
                Direction.West => new Coordinate(Player.Spot.X - 1, Player.Spot.Y),
                _ => throw new ArgumentOutOfRangeException($"The direction you gave as input was not found. Did you add a new direction to the Direction enum?")
            };
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
            //TODO: check if coordinate is door
            return false;
        }
    }
}
