using System;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Enums;

namespace CODE_GameLib.Tiles
{
    public class ConveyorBelt
    {
        public Direction Direction;
        public Coordinate Coordinate;

        public ConveyorBelt(Direction direction, Coordinate coordinate)
        {
            Direction = direction;
            Coordinate = coordinate;
        }

        public Coordinate ApplyEffect(Coordinate coordinate)
        {
            switch (Direction)
            {
                case Direction.North:
                    coordinate.Y--;
                    return coordinate;
                case Direction.East:
                    coordinate.X++;
                    return coordinate;
                case Direction.South:
                    coordinate.Y++;
                    return coordinate;
                case Direction.West:
                    coordinate.X--;
                    return coordinate;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public static void MoveEntities(Player player)
        {
            var room = player.CurrentRoom;

            var beltUnderPlayer =
                room.Belts.FirstOrDefault(b => b.Coordinate.X == player.Spot.X && b.Coordinate.Y == player.Spot.Y);
            // Move player
            if (beltUnderPlayer != null)
            {
                player.Spot = beltUnderPlayer.ApplyEffect(player.Spot);
            }
        }
    }
}