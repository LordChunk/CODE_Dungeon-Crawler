using CODE_GameLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Items.Adapter;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib
{
    public class Player
    {
        public Room CurrentRoom;

        public Coordinate Spot;

        public int Lives;
        private readonly int _gunDamage;
        public readonly List<IPickUpItem> Items;
        private List<Coordinate> ShootRange;

        public Player(Coordinate startSpot, Room currentRoom, int lives)
        {
            Spot = startSpot;
            Items = new List<IPickUpItem>();
            CurrentRoom = currentRoom;
            Lives = lives;
            _gunDamage = 1;
            ShootRange = new List<Coordinate>()
            {
                new Coordinate(Spot.X - 1, Spot.Y),
                new Coordinate(Spot.X + 1, Spot.Y),
                new Coordinate(Spot.X, Spot.Y - 1),
                new Coordinate(Spot.X - 1, Spot.Y + 1)
            };
        }

        public void Shoot()
        {
            foreach (var enemy in EnemiesNearby())
            {
                enemy.Adaptee.GetHurt(_gunDamage);
                if (enemy.Adaptee.NumberOfLives <= 0)
                {
                    CurrentRoom.Items.Remove(enemy);
                }
            }
        }

        private IEnumerable<EnemyAdapter> EnemiesNearby()
        {
            List<EnemyAdapter> list = new List<EnemyAdapter>();
            foreach (var item in CurrentRoom.Items)
            {
                if (item.GetType() == typeof(EnemyAdapter))
                {
                    EnemyAdapter enemy = (EnemyAdapter)item;
                    if (IsEnemyClose(enemy)) list.Add(enemy);
                }
            }

            return list;
        }

        private bool IsEnemyClose(EnemyAdapter enemy)
        {
            var enemyLocation = new Coordinate(enemy.Adaptee.CurrentXLocation, enemy.Adaptee.CurrentYLocation);

            return ShootRange.Any(coordinate => enemyLocation.IsEqual(coordinate));
        }
    }
}