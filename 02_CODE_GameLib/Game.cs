using System;
using CODE_GameLib.Enums;

namespace CODE_GameLib
{
    public class Game
    {
        public event EventHandler<Game> Updated;
        public Player Player;


        //wanneer game aangepast wordt:
        //Updated?.Invoke(this, this);
        protected virtual void OnUpdated(Game game)
        {
            Updated?.Invoke(this, game);
        }

        public void MovePlayer(Direction direction)
        {
            Updated?.Invoke(this, this);
        }
    }
}
