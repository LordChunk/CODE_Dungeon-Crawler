using System.Linq;
using CODE_GameLib.Doors;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class PressurePlate : Item, IEnvironmentalItem
    {
        public bool IsActive { get; private set; }

        public PressurePlate() : base(ItemType.PressurePlate)
        {
            IsActive = false;
        }

        public void OnTrigger(Player player)
        {
            var toggleDoors = player.CurrentRoom.Connections
                .Where(d => d.GetType() == typeof(ToggleConnection))
                .Select(d => d as ToggleConnection);

            foreach (var toggleDoor in toggleDoors)
            {
                toggleDoor?.Toggle();
            }

            IsActive = !IsActive;
        }
    }
}