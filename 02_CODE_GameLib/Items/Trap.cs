using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class Trap : Item, IEnvironmentalItem
    {
        public int Damage;

        public virtual void OnTrigger(Player player)
        {
            player.Lives -= Damage;
        }
    }
}