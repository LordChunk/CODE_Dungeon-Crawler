using System.Collections.Generic;
using CODE_GameLib.Doors.Common;
using CODE_GameLib.Enums;

namespace CODE_GameLib.Interfaces
{
    public interface IConnection
    {
        public Dictionary<Direction, Room> Rooms { get; set; }
        bool CanUseDoor(Player player);
        bool UseDoor(Player player);
    }
}