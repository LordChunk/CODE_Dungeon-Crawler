using System.Collections.Generic;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib
{
    public class Player
    {
        public Room CurrentRoom;

        public Coordinate Spot;

        public int Lives;
        public readonly List<IPickUpItem> Items;

        public Player(Coordinate startSpot)
        {
            Spot = startSpot;
            Items = new List<IPickUpItem>();
        }
    }
}