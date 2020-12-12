using CODE_GameLib;
using System;
using System.Collections.Generic;
using CODE_GameLib.Enums;

namespace CODE_Frontend
{
    public class GameInputs
    {
        private static ConsoleKey _keyPressed;
        private static bool _quit;
        private readonly Dictionary<ConsoleKey, Action> _moveKeys;
        private readonly Game _game;
        private readonly GameView _gameView;

        public GameInputs(GameView gameView, Game game)
        {
            _gameView = gameView;
            _game = game;
            _quit = false;
            _moveKeys = new Dictionary<ConsoleKey, Action>
            {
                {ConsoleKey.UpArrow, () => game.MovePlayer(Direction.North)},
                {ConsoleKey.LeftArrow, () => game.MovePlayer(Direction.West)},
                {ConsoleKey.RightArrow, () => game.MovePlayer(Direction.East)},
                {ConsoleKey.DownArrow, () => game.MovePlayer(Direction.South)},
                {ConsoleKey.W, () => game.MovePlayer(Direction.North)},
                {ConsoleKey.A, () => game.MovePlayer(Direction.West)},
                {ConsoleKey.D,() =>  game.MovePlayer(Direction.East)},
                {ConsoleKey.S, () => game.MovePlayer(Direction.South)},
                {ConsoleKey.Escape, () => _gameView.DrawEnd()}
            };
        }

        public void Run()
        {
            _game.Updated += (sender, game1) => _gameView.Draw(game1);

            while (!_quit)
            {
                _keyPressed = Console.ReadKey().Key;
                _quit = _keyPressed == ConsoleKey.Escape;

                if (_moveKeys.TryGetValue(_keyPressed, out var action))
                {
                    action.Invoke();
                    
                    _keyPressed = 0;
                }
            }

            Console.ReadLine();
        }
    }
}