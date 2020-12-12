namespace CODE_GameLib.Interfaces
{
    public interface IPickUpItem
    {
        public bool IsPickedUp { get; set; }
        public void OnPickUp(Player player);
    }
}