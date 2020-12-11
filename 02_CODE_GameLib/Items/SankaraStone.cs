using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class SankaraStone : Item, IPickUpItem
    {
        public void OnPickUp(Player player)
        {
            player.Items.Add(this);
        }

        public override void OnTouch(Player player)
        {
            OnPickUp(player);
        }
    }
}