using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors.Common
{
    public abstract class Door : IDoor
    {
        public int RoomId { get; }
        public Direction Direction { get; }
        public Door LinkedDoor { get; private set; }

        public Door(int roomId, Direction direction)
        {
            RoomId = roomId;
            Direction = direction;
        }

        public bool LinkDoorToDoor(Door toLinkDoor)
        {
            if (LinkedDoor != null) return false; 
            LinkedDoor = toLinkDoor;
            return false;
        }

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