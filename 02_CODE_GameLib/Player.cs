using System.Collections.Generic;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib
{
    public class Player
    {
        public Room CurrentRoom = new Room{Height = 5, Width = 5};
        public int X;
        public int Y;

        public int Lives;
        public IEnumerable<IPickUpItem> Items;
    }
}