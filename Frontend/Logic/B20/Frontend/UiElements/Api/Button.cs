using System;
using B20.Architecture.Exceptions;

namespace B20.Frontend.UiElements
{
    public class ButtonOnClickNotSetException : ApiException
    {
        public ButtonOnClickNotSetException(string message) : base(message)
        {
        }
    }
    
    public class Button: UiElement<EmptyModel>
    {
        private Action onClick;
        
        public void Click()
        {
            if (onClick == null)
            {
                throw new ButtonOnClickNotSetException("Button OnClick not set");
            }
            onClick();
        }

        public void OnClick(Action onClick)
        {
            this.onClick = onClick;
        }
    }
}
