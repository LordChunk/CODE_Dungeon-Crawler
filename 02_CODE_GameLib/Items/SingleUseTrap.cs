namespace CODE_GameLib.Items
{
    public class SingleUseTrap : Trap
    {
        public SingleUseTrap(Coordinate coordinate, int damage) : base(coordinate, damage)
        {
            IsUsed = false;
        }

        public bool IsUsed { get; private set; }

        public override void OnTrigger(Player player)
        {
            if (!IsUsed)
            {
                base.OnTrigger(player);
                IsUsed = true;
            }
        }
    }
}