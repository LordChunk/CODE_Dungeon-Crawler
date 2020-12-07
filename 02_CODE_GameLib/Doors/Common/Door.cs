namespace CODE_GameLib.Doors.Common
{
    public abstract class Door
    {
        public Direction Direction;
        public Door LinkedDoor;
        public Room LinkedRoom;
    }
}