namespace B20.Frontend.Postion
{
    public class Rect
    {
        private Position2D _topLeft;
        private Position2D _bottomRight;
         
        public Rect(int topLeftX, int topLeftY, int bottomRightX, int bottomRightY)
            : this(new Position2D(topLeftX, topLeftY), new Position2D(bottomRightX, bottomRightY))
        {
        }
        
        public Rect(Position2D topLeft, Position2D bottomRight)
        {
            _topLeft = topLeft;
            _bottomRight = bottomRight;
        }
            
        public bool IsInside(Position2D p)
        {
            return p.X >= _topLeft.X && p.X <= _bottomRight.X &&
                   p.Y >= _topLeft.Y && p.Y <= _bottomRight.Y;
        }
    }
}