using B20.Architecture.Exceptions;
using B20.Frontend.Element;
using B20.Frontend.Postion;

namespace B20.Frontend.Traits
{
    public class RectNotSetException: ApiException
    {
        public RectNotSetException(): base("Rect not set")
        {
        }
    }
    
    public class WithRect: Trait
    {
        public Rect Rect { get; set; }
        
        public bool IsInside(Position2D p)
        {
            if (Rect == null)
            {
                throw new RectNotSetException();
            }
            return Rect.IsInside(p);
        }
    }
}