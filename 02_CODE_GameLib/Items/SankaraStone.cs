using CODE_GameLib.Enums;
using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class SankaraStone : Item, IPickUpItem
    {
        public SankaraStone() : base(ItemType.SankaraStone)
        {
        }
    }
}