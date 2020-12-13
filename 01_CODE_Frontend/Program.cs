using CODE_PersistenceLib;
using System;
using System.Text;

namespace CODE_Frontend
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.WindowWidth = 200;
            Console.WindowHeight = 50;
            Console.CursorVisible = false;

            var reader = new GameReader();
            var game = reader.Read(@"./Levels/TempleOfDoom.json");

            var gameView = new GameView();

            gameView.Draw(game);

            var inputReader = new GameInputs(gameView, game);
            inputReader.Run();
        }
    }
}