using System;
using CODE_GameLib.Enums;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public Player Player;


        //wanneer game aangepast wordt:
        //Updated?.Invoke(this, this);

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
                
                Updated?.Invoke(this, this);
            }
        }

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
            switch (direction)
            {
                case Direction.North:
                    return new Coordinate(Player.Spot.X, Player.Spot.X - 1);
                case Direction.East:
                    return new Coordinate(Player.Spot.X + 1, Player.Spot.X);
                case Direction.South:
                    return new Coordinate(Player.Spot.X, Player.Spot.X + 1);
                case Direction.West:
                    return new Coordinate(Player.Spot.X - 1, Player.Spot.X);
            }

            return null;
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
