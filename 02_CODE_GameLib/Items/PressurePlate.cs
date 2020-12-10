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
            var toggleDoors = player.CurrentRoom.Doors
                .Where(d => d.GetType() == typeof(ToggleDoor))
                .Select(d => d.Value as ToggleDoor);

            foreach (var toggleDoor in toggleDoors)
            {
                toggleDoor?.Toggle();
            }

            IsActive = !IsActive;
        }
    }
}