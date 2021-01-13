using CODE_GameLib.Interfaces;
using System.Collections.Generic;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib
{
    public class Room
    {
        public int Id;
        public int Width;
        public int Height;
        public List<IItem> Items;
        public List<Enemy> Enemies;
        public Dictionary<Coordinate, IDoor> Connections;
        public readonly string Type = "room";

        public Room(int id, int height, int width, Dictionary<Coordinate, IDoor> connections, List<IItem> items, List<Enemy> enemies)
        {
            Id = id;
            Height = height;
            Width = width;
            Connections = connections;
            Items = items;
            Enemies = enemies;
        }
    }
}