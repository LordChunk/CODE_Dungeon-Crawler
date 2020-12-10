using System.Collections.Generic;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id;
        public int Width;
        public int Height;
        public List<IItem> Items;
        public Dictionary<Direction, IDoor> Doors;
        public readonly string Type = "room";

        public Room()
        {
            Doors = new Dictionary<Direction, IDoor>();
        }
    }
}