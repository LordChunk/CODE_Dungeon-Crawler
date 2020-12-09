using CODE_GameLib;
using System;
using System.Text;
using CODE_PersistenceLib;

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

            GameReader reader = new GameReader();
            Game game = reader.Read(@"./Levels/TempleOfDoom.json");

            GameView gameView = new GameView();

            gameView.Draw(game);

            //game.Updated += (sender, game) => 
            GameInputs inputReader = new GameInputs();
            inputReader.Run(gameView, game);
        }
    }
}