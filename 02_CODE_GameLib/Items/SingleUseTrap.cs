namespace CODE_GameLib.Items
{
    public class SingleUseTrap : Trap
    {
        public bool IsUsed { get; private set; }

        public SingleUseTrap(Coordinate coordinate, int damage) : base(coordinate,damage)
        {
            IsUsed = false;
        }

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