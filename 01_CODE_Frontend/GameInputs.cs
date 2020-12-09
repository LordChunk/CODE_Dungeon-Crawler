using CODE_GameLib;
using System;

namespace CODE_Frontend
{
    public class GameInputs
    {
        

        private static ConsoleKey _keyPressed;
        private static bool _quit = false;

        public void Run(GameView gameView, Game game)
        {
            _keyPressed = Console.ReadKey().Key;
            _quit = _keyPressed == ConsoleKey.Escape;

            while (!_quit)
            {
                game.Updated += (sender, game1) => gameView.Draw(game1);

                //Console.WriteLine($"\nYou pressed {keyPressed}");

                _keyPressed = Console.ReadKey().Key;
                _quit = _keyPressed == ConsoleKey.Escape;
            }
        }
    }
}
