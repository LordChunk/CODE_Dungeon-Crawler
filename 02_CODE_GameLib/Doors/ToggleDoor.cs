using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors
{
    public class ToggleDoor : Door, ILockable
    {
        public bool IsLocked { get; private set; }

        public ToggleDoor() 
        {
            IsLocked = false;
        }

        public void Toggle()
        {
            IsLocked = !IsLocked;
        }

        public override bool CanUseDoor(Player player)
        {
            return !IsLocked;
        }
    }
}