namespace CODE_GameLib.Interfaces
{
    public interface IItem
    {
        public Coordinate Coordinate { get; set; }

        public void OnTouch(Player player);
    }
}