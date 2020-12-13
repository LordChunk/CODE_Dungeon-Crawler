using CODE_GameLib.Enums;

namespace CODE_GameLib.Interfaces
{
    public interface IDoor
    {
        public Direction Location { get; set; }
        public Room IsInRoom { get; set; }
        public IDoor ConnectsToDoor { get; set; }

        public bool CanUseDoor(Player player);
        public bool UseDoor(Player player);
    }
}