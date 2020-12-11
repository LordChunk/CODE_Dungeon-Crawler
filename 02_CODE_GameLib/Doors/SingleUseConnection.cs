using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors
{
    public class SingleUseConnection : Connection, ILockable
    {
        public bool IsLocked { get; private set; }

        public SingleUseConnection()
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