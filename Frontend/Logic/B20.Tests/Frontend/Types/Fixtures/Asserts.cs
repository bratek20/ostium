using B20.Frontend.Postion;
using B20.Tests.ExtraAsserts;

namespace B20.Tests.Frontend.Types.Fixtures
{
    public class AssertTypes
    {
        public static void Position2d(Position2d p, int x, int y)
        {
            AssertExt.Equal(p.X, x);
            AssertExt.Equal(p.Y, y);
        }
        
        public static void Rect(Rect r, int leftBottomX, int leftBottomY, int width, int height)
        {
            AssertExt.Equal(r.LeftBottom.X, leftBottomX);
            AssertExt.Equal(r.LeftBottom.Y, leftBottomY);
            AssertExt.Equal(r.Size.X, width);
            AssertExt.Equal(r.Size.Y, height);
        }
    }
}