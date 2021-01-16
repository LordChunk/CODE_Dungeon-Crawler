using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class SankaraStone : Item, IPickUpItem
    {
        public bool IsPickedUp { get; set; }

        public SankaraStone(Coordinate coordinate) : base(coordinate)
        {
            IsPickedUp = false;
        }
        
        public override void OnTouch(Player player)
        {
            if (IsPickedUp) return;
            player.Items.Add(this);
            IsPickedUp = true;
        }
    }
}