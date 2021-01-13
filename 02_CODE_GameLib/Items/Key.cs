using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;
using System.Drawing;
using CODE_GameLib.Services;

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
        
        public override void OnTouch(Player player)
        {
            if (IsPickedUp) return;
            player.Items.Add(this);
            IsPickedUp = true;
        }
    }
}