using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;
using System.Drawing;

namespace CODE_GameLib.Items
{
    public class Key : Item, IPickUpItem, IColorBinder
    {
        public Color ColorCode { get; set; }

        public bool IsPickedUp { get; set; }

        public Key(Coordinate coordinate, Color colorCode) : base(coordinate)
        {
            ColorCode = colorCode;
            IsPickedUp = false;
        }

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