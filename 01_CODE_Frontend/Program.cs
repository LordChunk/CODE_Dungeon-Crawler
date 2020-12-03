using CODE_FileSystem;
using CODE_GameLib;
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

            GameReader reader = new GameReader();
            Game game = reader.Read(@"./Levels/TempleOfDoom.json");

            GameView gameView = new GameView();
            game.Updated += (sender, game) => gameView.Draw(game);
            game.Run();
        }
    }
}