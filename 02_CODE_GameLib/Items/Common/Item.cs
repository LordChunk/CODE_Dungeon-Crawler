﻿using CODE_GameLib.Interfaces;

namespace CODE_GameLib.Items.Common
{
    public abstract class Item : IItem
    {
        public Coordinate Coordinate { get; set; }

        public Item(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }

        public abstract void OnTouch(Player player);
    }
}