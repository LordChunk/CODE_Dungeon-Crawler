using System.Collections.Generic;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors.Common
{
    public class Connection : IConnection
    {
        public List<RoomDirectionPair> Rooms { get; set; }

        public virtual bool CanUseDoor(Player player)
        {
            return true;
        }

        public virtual bool UseDoor(Player player)
        {
            return CanUseDoor(player);
        }

        public struct RoomDirectionPair
        {
            public Direction DoorLocation;
            public Room Room;
        }
    }
}