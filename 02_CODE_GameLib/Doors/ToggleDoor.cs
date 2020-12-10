using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors
{
    public class ToggleDoor : Door
    {
        public bool Toggled { get; private set; }

        public ToggleDoor(int roomId, Direction direction) : base(roomId, direction)
        {
            Toggled = false;
        }

        public void Toggle()
        {
            Toggled = !Toggled;
        }
    }
}