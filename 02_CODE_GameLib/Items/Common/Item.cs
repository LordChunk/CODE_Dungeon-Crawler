using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Items.Common
{
    public abstract class Item : IItem
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}