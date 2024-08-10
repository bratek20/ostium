namespace B20.Frontend.Postion
{
    public class Rect
    {
        private Position2D leftBottom;
        private Position2D size;
        
        public Rect(int leftBottomX, int leftBottomY, int width, int height)
            : this(new Position2D(leftBottomX, leftBottomY), new Position2D(width, height))
        { }
        
        public Rect(Position2D leftBottom, Position2D size)
        {
            this.leftBottom = leftBottom;
            this.size = size;
        }
        
        public bool IsInside(Position2D p)
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