using System;
using B20.Frontend.UiElements;
using B20.Frontend.Postion;

namespace B20.Frontend.UiElements
{
    public class WithPosition2d : Trait
    {
        private Action<Position2d> _positionSetter;

        public void SetPositionSetter(Action<Position2d> positionSetter)
        {
            _positionSetter = positionSetter;
        }
        
        public Position2d Model { get; private set; }

        public void Update(Position2d p)
        {
            Model = p;
            _positionSetter?.Invoke(p);
        }
    }
}