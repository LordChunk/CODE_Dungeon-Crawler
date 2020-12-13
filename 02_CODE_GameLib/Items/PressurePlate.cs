using CODE_GameLib.Doors;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;
using System.Linq;

namespace CODE_GameLib.Items
{
    public class PressurePlate : Item, IEnvironmentalItem
    {
        public bool IsActive { get; private set; }

        public PressurePlate(Coordinate coordinate) : base(coordinate)
        {
            IsActive = false;
        }

        public void OnTrigger(Player player)
        {
            var toggleDoors = player.CurrentRoom.Connections
                .Where(d => d.GetType() == typeof(ToggleDoor))
                .Select(d => d.Value as ToggleDoor);

            foreach (var toggleDoor in toggleDoors)
            {
                toggleDoor?.Toggle();
            }

            IsActive = !IsActive;
        }

        public override void OnTouch(Player player)
        {
            OnTrigger(player);
        }
    }
}