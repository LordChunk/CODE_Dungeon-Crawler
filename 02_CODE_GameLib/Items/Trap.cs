using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;
using CODE_GameLib.Services;

namespace CODE_GameLib.Items
{
    public class Trap : Item, IEnvironmentalItem
    {
        public int Damage;
        
        public override void OnTouch(Player player)
        {
            if (_cheatService.LoseNoLives)
                return;
            player.Lives -= Damage;
        }

        public Trap(Coordinate coordinate, CheatService cheatService, int damage) : base(coordinate,cheatService)
        {
            Damage = damage;
        }
    }
}