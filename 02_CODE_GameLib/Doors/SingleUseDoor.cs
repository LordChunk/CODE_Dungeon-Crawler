using CODE_GameLib.Doors.Common;
using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Doors
{
    public class SingleUseDoor : Door, ILockable
    {
        public SingleUseDoor()
        {
            IsLocked = false;
        }

        public bool IsLocked { get; set; }

        // Lock both doors
        public void Lock()
        {
            IsLocked = true;
            if (ConnectsToDoor.GetType() == typeof(SingleUseDoor))
                ((SingleUseDoor) ConnectsToDoor).IsLocked = true;
        }

        public override bool CanUseDoor(Player player)
        {
            return !IsLocked;
        }

        public override bool UseDoor(Player player)
        {
            if (!CanUseDoor(player))
            {
                if (!CheatsService.ClosedClosingGateResets) return false;
                player.Reset();
                return true;
            }

            base.UseDoor(player);
            Lock();
            return true;
        }
    }
}