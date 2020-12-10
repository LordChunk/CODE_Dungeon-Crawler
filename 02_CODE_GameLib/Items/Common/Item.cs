using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Items.Common
{
    public abstract class Item : IItem
    {
        public Coordinate Coordinate { get; set; }
    }
}