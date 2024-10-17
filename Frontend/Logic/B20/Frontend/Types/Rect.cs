namespace B20.Frontend.Postion
{
    public class Rect
    {
        public Position2d LeftBottom { get; }
        public Position2d Size { get; }
        
        public Rect(int leftBottomX, int leftBottomY, int width, int height)
            : this(new Position2d(leftBottomX, leftBottomY), new Position2d(width, height))
        { }
        
        public Position2d Center => new Position2d(LeftBottom.X + Size.X / 2, LeftBottom.Y + Size.Y / 2);
        
        public Rect(Position2d leftBottom, Position2d size)
        {
            LeftBottom = leftBottom;
            Size = size;
        }
        
        public bool IsInside(Position2d p)
        {
            return LeftBottom.X <= p.X && p.X <= LeftBottom.X + Size.X &&
                   LeftBottom.Y <= p.Y && p.Y <= LeftBottom.Y + Size.Y;
        }

        public override string ToString()
        {
            return $"(lbx: {LeftBottom.X}, lby: {LeftBottom.Y}, w: {Size.X}, h: {Size.Y})";
        }
    }
}