using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using System.Collections.Generic;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id;
        public int Width;
        public int Height;
        public List<IItem> Items;
        public Dictionary<Coordinate, IDoor> Connections;
        public readonly string Type = "room";

        public Room(int id, int height, int width, Dictionary<Coordinate, IDoor> connections)
        {
            Id = id;
            Height = height;
            Width = width;
            Connections = connections;
            Items = new List<IItem>();
        }
    }
}