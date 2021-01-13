using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class SankaraStone : Item, IPickUpItem
    {
        public SankaraStone(Coordinate coordinate) : base(coordinate)
        {
            IsPickedUp = false;
        }

        public bool IsPickedUp { get; set; }

        public void OnPickUp(Player player)
        {
            if (!IsPickedUp)
            {
                player.Items.Add(this);
                IsPickedUp = true;
            }
        }

        public override void OnTouch(Player player)
        {
            OnPickUp(player);
        }
    }
}