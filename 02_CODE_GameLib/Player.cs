using System;
using CODE_GameLib.Interfaces;
using System.Collections.Generic;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib
{
    public class Player
    {
        public Room CurrentRoom;
        public Coordinate Spot;

        public int Lives;
        public readonly List<IPickUpItem> Items;

        public Player(Coordinate startSpot, Room currentRoom, int lives)
        {
            Spot = startSpot;
            Items = new List<IPickUpItem>();
            CurrentRoom = currentRoom;
            Lives = lives;
        }

        public void UpdateCurrentRoom(Room room)
        {
            CurrentRoom = room;
        }

        public void Attack()
        {
            var removable = new List<Enemy>();
            foreach (var enemy in CurrentRoom.Enemies)
            {
                // Check if enemy is one block away on X or Y axis
                if (
                    ((enemy.CurrentXLocation - 1 == Spot.X || enemy.CurrentXLocation + 1 == Spot.X) &&
                     enemy.CurrentYLocation == Spot.Y) || 
                    ((enemy.CurrentYLocation - 1 == Spot.Y || enemy.CurrentYLocation + 1 == Spot.Y) &&
                     enemy.CurrentXLocation == Spot.X)
                )
                {
                    enemy.GetHurt(1);
                    if (enemy.NumberOfLives <= 0)
                    {
                        removable.Add(enemy);
                    }
                }
            }

            foreach (var enemy in removable)
            {
                CurrentRoom.Enemies.Remove(enemy);
            }
        }
    }
}