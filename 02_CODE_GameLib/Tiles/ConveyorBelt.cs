using System;
using System.Linq;
using CODE_GameLib.Enums;

namespace CODE_GameLib.Tiles
{
    public class ConveyorBelt
    {
        public Coordinate Coordinate;
        public Direction Direction;

        public ConveyorBelt(Direction direction, Coordinate coordinate)
        {
            Direction = direction;
            Coordinate = coordinate;
        }

        public Coordinate ApplyEffect(Coordinate coordinate, Room room)
        {
            switch (Direction)
            {
                case Direction.North:
                    return Game.CalcTargetCoordinate(Direction.North, coordinate, room);
                case Direction.East:
                    return Game.CalcTargetCoordinate(Direction.East, coordinate, room);
                case Direction.South:
                    return Game.CalcTargetCoordinate(Direction.South, coordinate, room);
                case Direction.West:
                    return Game.CalcTargetCoordinate(Direction.West, coordinate, room);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        ///     Move all entities on conveyor belts in the room in which the player is playing.
        /// </summary>
        /// <param name="player"></param>
        public static void MoveEntities(Player player)
        {
            var room = player.CurrentRoom;

            var beltUnderPlayer =
                room.Belts.FirstOrDefault(b => b.Coordinate.X == player.Spot.X && b.Coordinate.Y == player.Spot.Y);
            // Move player
            if (beltUnderPlayer != null)
                player.Spot = beltUnderPlayer.ApplyEffect(player.Spot, player.CurrentRoom);


            // Move enemies
            foreach (var enemy in room.Enemies)
            {
                // Get belt on the same coordinate as enemy
                var beltUnderEnemy = room.Belts.FirstOrDefault(b =>
                    b.Coordinate.X == enemy.CurrentXLocation && b.Coordinate.Y == enemy.CurrentYLocation);

                if (beltUnderEnemy == null) continue;
                var coordinate =
                    beltUnderEnemy.ApplyEffect(new Coordinate(enemy.CurrentXLocation, enemy.CurrentYLocation), room);
                enemy.CurrentXLocation = coordinate.X;
                enemy.CurrentYLocation = coordinate.Y;
            }
        }
    }
}