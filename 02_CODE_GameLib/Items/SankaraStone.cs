using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;
using CODE_GameLib.Services;

namespace CODE_GameLib.Items
{
    public class SankaraStone : Item, IPickUpItem
    {
        public bool IsPickedUp { get; set; }

        public SankaraStone(Coordinate coordinate, CheatService cheatService) : base(coordinate,cheatService)
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