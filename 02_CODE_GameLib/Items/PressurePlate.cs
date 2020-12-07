using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class PressurePlate : Item, IEnvironmentalItem
    {
        public bool IsActive;
        public void OnTrigger()
        {
            throw new System.NotImplementedException();
        }
    }
}