namespace B20.Frontend.Postion
{
    public class Rect
    {
        private Position2d leftBottom;
        private Position2d size;
        
        public Rect(int leftBottomX, int leftBottomY, int width, int height)
            : this(new Position2d(leftBottomX, leftBottomY), new Position2d(width, height))
        { }
        
        public Rect(Position2d leftBottom, Position2d size)
        {
            this.leftBottom = leftBottom;
            this.size = size;
        }
        
        public bool IsInside(Position2d p)
        {
            return leftBottom.X <= p.X && p.X <= leftBottom.X + size.X &&
                   leftBottom.Y <= p.Y && p.Y <= leftBottom.Y + size.Y;
        }

        public override string ToString()
        {
            return $"(lbx: {leftBottom.X}, lby: {leftBottom.Y}, w: {size.X}, h: {size.Y})";
        }
    }
}