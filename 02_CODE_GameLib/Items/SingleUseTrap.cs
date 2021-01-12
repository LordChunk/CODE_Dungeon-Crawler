using CODE_GameLib.Services;

namespace CODE_GameLib.Items
{
    public class SingleUseTrap : Trap
    {
        public bool IsUsed { get; private set; }

        public SingleUseTrap(Coordinate coordinate, CheatService cheatService, int damage) : base(coordinate, cheatService, damage)
        {
            IsUsed = false;
        }

        public override void OnTouch(Player player)
        {
            if (IsUsed) return;
            base.OnTouch(player);
            IsUsed = true;
        }
    }
}