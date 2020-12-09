using CODE_GameLib;
using System;

namespace CODE_Frontend
{
    public class GameInputs
    {
        private static event EventHandler<Game> Updated;

        private static ConsoleKey _keyPressed;
        private static bool _quit = false;

        private void Run(GameView gameView, Game game)
        {
            _keyPressed = Console.ReadKey().Key;
            _quit = _keyPressed == ConsoleKey.Escape;

            while (!_quit)
            {
                Updated?.Invoke(Updated, game);

                _keyPressed = Console.ReadKey().Key;
                _quit = _keyPressed == ConsoleKey.Escape;
            }

            Updated?.Invoke(Updated, game);
        }
    }
}
