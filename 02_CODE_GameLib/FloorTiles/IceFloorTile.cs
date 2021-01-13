using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.FloorTiles
{
    public class IceFloorTile : IFloorTile
    {
        public Coordinate Coordinate { get; set; }

        public IceFloorTile(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }
}
