using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors.Common
{
    public class Door : IDoor
    {
        public Coordinate Coordinate { get; set; }
        public Room IsInRoom { get; set; }
        public IDoor ConnectsToDoor { get; set; }

        public virtual bool CanUseDoor(Player player)
        {
            return true;
        }

        public virtual bool UseDoor(Player player)
        {
            if (!CanUseDoor(player)) return false;
            player.CurrentRoom = IsInRoom;
            player.Spot = Coordinate;
            return true;
        }
    }
}