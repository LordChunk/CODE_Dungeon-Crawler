namespace CODE_GameLib.Items
{
    public class SingleUseTrap : Trap
    {
        public bool IsUsed { get; private set; }

        public override void OnTrigger()
        {
            base.OnTrigger();
            IsUsed = true;
        }
    }
}