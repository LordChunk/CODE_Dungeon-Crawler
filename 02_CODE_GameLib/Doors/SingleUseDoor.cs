using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors
{
    public class SingleUseDoor : Door, ILockable
    {
        public bool IsLocked { get; private set; }

        public SingleUseDoor()
        {
            IsLocked = false;
        }

        public override bool CanUseDoor(Player player)
        {
            return !IsLocked;
        }

        public override bool UseDoor(Player player)
        {
            if (!CanUseDoor(player)) return false;
            IsLocked = true;
            return true;
        }
    }
}