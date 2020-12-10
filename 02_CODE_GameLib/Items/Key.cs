using System.Drawing;
using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class Key : Item, IPickUpItem, IColorBinder
    {
        public Color ColorCode { get; set; }
    }
}