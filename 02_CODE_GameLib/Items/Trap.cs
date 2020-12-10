using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class Trap : Item, IEnvironmentalItem
    {
        public readonly int Damage;

        public Trap(int damage)
        {
            Damage = damage;
        }

        public virtual void OnTrigger(Player player)
        {
            player.Lives -= Damage;
        }
    }
}