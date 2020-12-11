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
            Updated?.Invoke(this, this);
        }

        private bool CanPlayerMove(Direction direction)
        {
            Coordinate targetCoordinate = null;

            switch (direction)
            {
                case Direction.North:
                    targetCoordinate = new Coordinate(Player.Spot.X, Player.Spot.X - 1);
                    break;
                case Direction.East:
                    targetCoordinate = new Coordinate(Player.Spot.X + 1, Player.Spot.X);
                    break;
                case Direction.South:
                    targetCoordinate = new Coordinate(Player.Spot.X, Player.Spot.X + 1);
                    break;
                case Direction.West:
                    targetCoordinate = new Coordinate(Player.Spot.X - 1, Player.Spot.X);
                    break;
            }

            if (IsCoordinateWall(targetCoordinate))
            {
                return false;
            }

            return true;
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
