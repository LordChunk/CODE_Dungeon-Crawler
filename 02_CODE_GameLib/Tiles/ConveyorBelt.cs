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
    }
}