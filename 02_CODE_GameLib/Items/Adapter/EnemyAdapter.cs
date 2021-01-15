using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using CODE_GameLib.Interfaces;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Items.Adapter
{
    public class EnemyAdapter : IItem
    {
        public Coordinate Coordinate
        {
            get => new Coordinate(Adaptee.CurrentXLocation,Adaptee.CurrentYLocation);
            set => new Coordinate(Adaptee.CurrentXLocation, Adaptee.CurrentYLocation);
        }

        public Enemy Adaptee { get;}

        public EnemyAdapter(Enemy adaptee)
        {
            Adaptee = adaptee;
        }

        public void OnTouch(Player player)
        {
            player.Lives -= 1;
        }
    }
}
