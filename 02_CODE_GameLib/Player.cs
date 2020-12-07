using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib
{
    public class Player
    {
        public Room CurrentRoom;
        public int X;
        public int Y;

        public int Lives;
        public IPickUpItem[] Items;
    }
}