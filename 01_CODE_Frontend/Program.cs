using CODE_PersistenceLib;
using System;
using System.Text;
using CODE_GameLib.Services;
using Microsoft.Extensions.DependencyInjection;

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

            var cheatSystem = new CheatService();

            var serviceProvider = new ServiceCollection().AddSingleton(cheatSystem)
                .BuildServiceProvider();

            var reader = new GameReader(cheatSystem);
            var game = reader.Read(@"./Levels/TempleOfDoom_Extended_A.json");

            var gameView = new GameView();

            gameView.Draw(game);

            var inputReader = new GameInputs(gameView, game, cheatSystem);
            inputReader.Run();
        }
    }
}