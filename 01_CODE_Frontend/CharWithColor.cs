using System;

namespace CODE_Frontend
{
    public class CharWithColor
    {
        public readonly char C;
        public readonly ConsoleColor Color;

        public CharWithColor(char c, ConsoleColor color)
        {
            C = c;
            Color = color;
        }
    }
}