using CODE_GameLib.Doors.Common;
using CODE_GameLib.Enums;

namespace CODE_GameLib.Interfaces
{
    public interface IDoor
    {
        int RoomId { get; }
        Direction Direction { get; }
        Door LinkedDoor { get; }
        bool LinkDoorToDoor(Door toLinkDoor);
    }
}