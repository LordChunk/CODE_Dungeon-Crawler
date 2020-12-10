using CODE_GameLib.Enums;

namespace CODE_GameLib.Interfaces
{
    public interface IItem
    {
        public Coordinate Coordinate { get; set; }

        public ItemType Type { get; set; }
    }
}