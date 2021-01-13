using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Items.Common
{
    public abstract class Item : IItem
    {
        public Item(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public Coordinate Coordinate { get; set; }

        public abstract void OnTouch(Player player);
    }
}