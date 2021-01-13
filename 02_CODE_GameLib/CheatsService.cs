using System.Linq;
using CODE_GameLib.Items;

namespace CODE_GameLib
{
    public class CheatsService
    {
        public static bool WinOnNextStone = false;
        public static bool ClosedClosingGateResets = false;

        private static int sankaraStoneStore;
        public static void ToggleWinOnNextStone(Game game)
        {
            if (!WinOnNextStone)
            {
                sankaraStoneStore = game.AmountOfSankaraStonesInGame;
                game.AmountOfSankaraStonesInGame = game.Player.Items.Count(i => i.GetType() == typeof(SankaraStone)) + 1;
                WinOnNextStone = true;
            }
            else
            {
                game.AmountOfSankaraStonesInGame = sankaraStoneStore;
                WinOnNextStone = false;
            }
        }
    }
}