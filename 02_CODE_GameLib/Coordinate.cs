namespace CODE_GameLib
{
    public class Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool IsEqual(Coordinate coordinate)
        {
            if (coordinate.X == X && coordinate.Y == Y)
                return true;

            return false;
        }
    }
}