namespace CODE_GameLib.Items
{
    public class SingleUseTrap : Trap
    {
        public bool IsUsed { get; private set; }

        public SingleUseTrap(int damage) : base(damage)
        {
            IsUsed = false;
        }

        public override void OnTrigger(Player player)
        {
            base.OnTrigger(player);
            IsUsed = true;
        }
    }
}