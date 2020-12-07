using System.Drawing;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class Key : Item, IPickUpItem, IColorBinder
    {
        public void OnPickUp()
        {
            throw new System.NotImplementedException();
        }

        public Color ColorCode { get; set; }
    }
}