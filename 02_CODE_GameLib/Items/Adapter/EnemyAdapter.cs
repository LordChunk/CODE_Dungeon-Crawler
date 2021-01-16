using CODE_GameLib.Interfaces;
using CODE_GameLib.Items.Common;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib.Items.Adapter
{
    public class EnemyAdapter : Item
    {
        private Enemy Adaptee { get;}
        public int NumberOfLives { get => Adaptee.NumberOfLives; }

        public EnemyAdapter(Enemy adaptee) : base(new Coordinate(adaptee.CurrentXLocation, adaptee.CurrentYLocation))
        {
            Adaptee = adaptee;
        }

        public override void OnTouch(Player player)
        {
            player.Lives -= 1;
        }

        public void Move()
        {
            Adaptee.Move();
            Coordinate.X = Adaptee.CurrentXLocation;
            Coordinate.Y = Adaptee.CurrentYLocation;
        }

        public void GetHurt(int gunDamage)
        {
            Adaptee.GetHurt(gunDamage);
        }
    }
}
