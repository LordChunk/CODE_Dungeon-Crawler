using CODE_PersistenceLib;
using System;
using System.Text;
using CODE_GameLib;
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

            var reader = new GameReader();
            var game = reader.Read(@"./Levels/TempleOfDoom_Extended_B.json");

            var gameView = new GameView();

            gameView.Draw(game);

            var inputReader = new GameInputs(gameView, game);
            inputReader.Run();

            var serviceProvider = new ServiceCollection()
                .AddSingleton(game.Player)
                .AddSingleton(new CheatsService())
                .BuildServiceProvider();
        }
    }
}