namespace CODE_GameLib.Items
{
    public class SingleUseTrap : Trap
    {
        public bool IsUsed { get; private set; }

        public SingleUseTrap(Coordinate coordinate, int damage) : base(coordinate, damage)
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