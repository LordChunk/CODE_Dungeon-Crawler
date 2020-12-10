using System.Collections.Generic;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors.Common
{
    public abstract class Connection : IConnection
    {
        public Dictionary<Direction, Room> Rooms { get; set; }

        public virtual bool CanUseDoor(Player player)
        {
            return true;
        }

        public virtual bool UseDoor(Player player)
        {
            return CanUseDoor(player);
        }
    }
}