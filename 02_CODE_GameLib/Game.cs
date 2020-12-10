using System;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;

        public Player Player = new Player();

        //wanneer game aangepast word:
        //Updated?.Invoke(this, this);

        public void MovePlayer(Direction direction)
        {
            Updated?.Invoke(this, this);
        }
    }
}
