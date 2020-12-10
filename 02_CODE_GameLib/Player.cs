using System.Collections.Generic;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib
{
    public class Player
    {
        public Room CurrentRoom;
        public int X;
        public int Y;

        public int Lives;
        public readonly List<IPickUpItem> Items;

        public Player()
        {
            Items = new List<IPickUpItem>();
        }
    }
}