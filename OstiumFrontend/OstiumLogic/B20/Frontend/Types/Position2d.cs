namespace B20.Frontend.Postion
{
    public class Position2d
    {
        public int X { get; }
        public int Y { get; }
        
        public Position2d(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }
    }
}