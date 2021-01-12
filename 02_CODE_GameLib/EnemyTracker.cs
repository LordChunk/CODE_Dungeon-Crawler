using System;
using System.Collections.Generic;
using CODE_TempleOfDoom_DownloadableContent;

namespace CODE_GameLib
{
    public class EnemyTracker : IObserver<Enemy>
    {
        private readonly Player _player;

        public EnemyTracker(List<Enemy> enemies, Player player)
        {
            _player = player;
            foreach (var enemy in enemies)
            {
                enemy.Subscribe(this);
            }
        }
        public void OnCompleted()
        {
            throw new NotImplementedException();
        }

        public void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        public void OnNext(Enemy enemy)
        {
            if (_player.CurrentRoom.Enemies.Contains(enemy))
            {
                if (_player.Spot.X == enemy.CurrentXLocation && _player.Spot.Y == enemy.CurrentYLocation)
                {
                    _player.Lives--;
                }
            }
        }
    }
}