using System.Collections.Generic;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Tiles;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib
{
    public class Room
    {
        public readonly string Type = "room";
        public List<ConveyorBelt> Belts;
        public Dictionary<Coordinate, IDoor> Connections;
        public List<Enemy> Enemies;
        public int Height;
        public int Id;
        public List<IItem> Items;
        public int Width;

        public Room(int id, int height, int width, Dictionary<Coordinate, IDoor> connections, List<IItem> items,
            List<Enemy> enemies, List<ConveyorBelt> belts)
        {
            Id = id;
            Height = height;
            Width = width;
            Connections = connections;
            Items = items;
            Enemies = enemies;
            Belts = belts;
        }
    }
}