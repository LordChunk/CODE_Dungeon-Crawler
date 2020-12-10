using System.Collections.Generic;
using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id { get; }
        public int Width { get; }
        public int Height { get; }
        public List<Item> Items { get; }
        public Dictionary<Direction, IDoor> Doors { get; }
        public readonly string Type = "room";

        public Room(int id, int width, int height, List<Item> items, Dictionary<Direction, IDoor> doors)
        {
            Items = items;
            Doors = doors;
            Id = id;
            Width = width;
            Height = height;
        }
    }
}