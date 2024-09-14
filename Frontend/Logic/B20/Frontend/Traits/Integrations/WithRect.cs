using System;
using B20.Architecture.Exceptions;
using B20.Frontend.UiElements;
using B20.Frontend.Postion;

namespace B20.Frontend.Traits
{
    public class RectProviderNotSetException: ApiException
    {
    }
    
    public class WithRect : Trait
    {
        private Func<Rect> _rectProvider;
        
        public void SetRectProvider(Func<Rect> rectProvider)
        {
            _rectProvider = rectProvider;
        }
        
        private Func<Rect> RectProvider
        {
            get 
            {
                if (_rectProvider == null)
                {
                    throw new RectProviderNotSetException();
                }
                return _rectProvider;
            }
        }

        public Rect Rect => RectProvider();
    
        public bool IsInside(Position2d p)
        {
            return Rect.IsInside(p);
        }
    }
}