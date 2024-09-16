using System;

namespace B20.Logic
{
    public class Button
    {
        private Action onClick;
        
        public void Click()
        {
            onClick();
        }

        public void OnClick(Action onClick)
        {
            this.onClick = onClick;
        }
    }
}
