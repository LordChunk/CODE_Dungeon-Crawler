using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class PressurePlate : Item, IEnvironmentalItem
    {
        public bool IsActive { get; private set; }

        public PressurePlate()
        {
            IsActive = false;
        }

        public void OnTrigger(Player player)
        {
            //player.CurrentRoom.;

            IsActive = !IsActive;
        }
    }
}