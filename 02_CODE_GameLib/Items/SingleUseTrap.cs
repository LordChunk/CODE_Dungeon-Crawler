using CODE_GameLib.Enums;

namespace CODE_GameLib.Items
{
    public class SingleUseTrap : Trap
    {
        public bool IsUsed { get; private set; }

        public SingleUseTrap()
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