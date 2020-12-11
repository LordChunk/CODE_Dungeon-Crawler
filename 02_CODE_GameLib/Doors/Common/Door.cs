using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors.Common
{
    public class Door : IDoor
    {
        public Direction Location { get; set; }
        public Room IsInRoom { get; set; }
        public IDoor ConnectsToDoor { get; set; }

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