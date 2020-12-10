namespace CODE_GameLib.Interfaces
{
    public interface IPickUpItem
    {
        public void OnPickUp(Player player)
        {
            player.Items.Add(this);
        }
    }
}