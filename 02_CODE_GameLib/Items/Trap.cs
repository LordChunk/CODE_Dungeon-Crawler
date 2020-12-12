﻿using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;

namespace CODE_GameLib.Items
{
    public class Trap : Item, IEnvironmentalItem
    {
        public int Damage;

        public virtual void OnTrigger(Player player)
        {
            player.Lives -= Damage;
        }

        public override void OnTouch(Player player)
        {
            OnTrigger(player);
        }

        public Trap(Coordinate coordinate, int damage) : base(coordinate)
        {
            Damage = damage;
        }
    }
}