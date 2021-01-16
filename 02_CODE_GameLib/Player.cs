using CODE_GameLib.Interfaces;
using System.Collections.Generic;
using System.Linq;
using CODE_GameLib.Items;
using CODE_GameLib.Items.Adapter;

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
            DetermineShootRange();
        }

        public void Shoot()
        {
            foreach (var enemy in EnemiesNearby())
            {
                enemy.GetHurt(_gunDamage);
                if (enemy.NumberOfLives <= 0)
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
            DetermineShootRange();
            foreach (var coordinate in ShootRange)
            {
                if (coordinate.IsEqual(enemy.Coordinate))
                {
                    return true;
                }
            }

            return false;
        }

        private void DetermineShootRange()
        {
            ShootRange = new List<Coordinate>()
            {
                new Coordinate(Spot.X - 1, Spot.Y),
                new Coordinate(Spot.X + 1, Spot.Y),
                new Coordinate(Spot.X, Spot.Y - 1),
                new Coordinate(Spot.X - 1, Spot.Y + 1)
            };
        }
    }
}