using CODE_GameLib;
using System;
using System.Collections.Generic;

namespace CODE_Frontend
{
    public class GameInputs
    {
        private static ConsoleKey _keyPressed;
        private static bool _quit;
        private readonly Dictionary<ConsoleKey,Direction> _moveKeys;

        public GameInputs()
        {
            _quit = false;
            _moveKeys = new Dictionary<ConsoleKey, Direction>
            {
                {ConsoleKey.UpArrow, Direction.North},
                {ConsoleKey.LeftArrow, Direction.West},
                {ConsoleKey.RightArrow, Direction.East},
                {ConsoleKey.DownArrow, Direction.South},
                {ConsoleKey.W, Direction.North},
                {ConsoleKey.A, Direction.West},
                {ConsoleKey.D, Direction.East},
                {ConsoleKey.S, Direction.South}
            };
        }

        public void Run(GameView gameView, Game game)
        {
            game.Updated += (sender, game1) => gameView.Draw(game1);

            while (!_quit)
            {
                if (_moveKeys.TryGetValue(_keyPressed, out var direction))
                {
                    game.MovePlayer(direction);
                    
                    _keyPressed = 0;
                }
                                

                _keyPressed = Console.ReadKey().Key;
                Console.WriteLine($"\nYou pressed {_keyPressed}");
                _quit = _keyPressed == ConsoleKey.Escape;
            }
        }
    }
}