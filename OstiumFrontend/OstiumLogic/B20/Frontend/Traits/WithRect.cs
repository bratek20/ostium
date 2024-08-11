using System;
using B20.Architecture.Exceptions;
using B20.Frontend.Element;
using B20.Frontend.Postion;

namespace B20.Frontend.Traits
{
    public class RectProviderNotSetException: ApiException
    {
    }
    
    public class WithRect : Trait
    {
        private Func<Rect> _rectProvider;

        public Func<Rect> RectProvider
        {
            get 
            {
                if (_rectProvider == null)
                {
                    throw new RectProviderNotSetException();
                }
                return _rectProvider;
            }
            set
            {
                _rectProvider = value;
            }
        }

        public Rect Rect => RectProvider();
    
        public bool IsInside(Position2d p)
        {
            return Rect.IsInside(p);
        }
    }
}