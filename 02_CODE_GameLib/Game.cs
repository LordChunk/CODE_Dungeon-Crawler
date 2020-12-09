using System;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;

        public Player Player;

        //wanneer game aangepast word:
        //Updated?.Invoke(this, this);
    }
}
