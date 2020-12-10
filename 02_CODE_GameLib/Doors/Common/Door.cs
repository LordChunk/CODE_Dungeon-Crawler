namespace CODE_GameLib.Doors.Common
{
    public abstract class Door
    {
        public Room IsInRoom;
        public Direction Direction;
        public Door LinkedDoor;
    }
}