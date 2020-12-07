namespace CODE_GameLib.Doors.Common
{
    public abstract class Door
    {
        public Direction Direction;
        public Room ConnectedRoom;
        public Direction ConnectedRoomSpawnDirection;
    }
}