using System;
using B20.Architecture.Exceptions;
using B20.Frontend.Element;
using B20.Frontend.Postion;

namespace B20.Frontend.Traits
{
    public class RectProviderNotSetException: ApiException
    {
    }
    
    public class WithRect: Trait
    {
        public Func<Rect> RectProvider { get; set; }
        public Rect Rect => RectProvider();
        
        public bool IsInside(Position2D p)
        {
            if (RectProvider == null)
            {
                throw new RectProviderNotSetException();
            }

            return RectProvider().IsInside(p);
        }
    }
}