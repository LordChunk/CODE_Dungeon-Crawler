using CODE_GameLib.Interfaces;
using CODE_GameLib.Services;

namespace CODE_GameLib.Items.Common
{
    public abstract class Item : IItem
    {
        public Coordinate Coordinate { get; set; }
        protected CheatService _cheatService;

        public Item(Coordinate coordinate, CheatService cheatService)
        {
            Coordinate = coordinate;
            _cheatService = cheatService;
        }

        public abstract void OnTouch(Player player);
    }
}