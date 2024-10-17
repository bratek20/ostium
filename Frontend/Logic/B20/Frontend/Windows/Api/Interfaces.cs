using System.Collections.Generic;
using B20.Frontend.UiElements;

namespace B20.Frontend.Windows.Api
{
    public interface Window
    {
        List<UiElement> Elements { get; }
    } 
    
    public abstract class Window<TState>: Window
    {
        protected TState State { get; private set; }
        public void Open(TState state)
        {
            State = state;
            OnOpen();
        }
        
        protected virtual void OnOpen() { }
        
        private List<UiElement> _elements;

        public List<UiElement> Elements
        {
            get
            {
                if (_elements == null)
                {
                    _elements = UiElementHelper.GetElementProperties(this);
                }
                return _elements;
            }
        }
    }

    public class EmptyWindowState
    {
    }

    // outgoing
    public interface WindowManager
    {
        T Get<T>() where T : class, Window;

        void Open<TWindow, TWindowState>(TWindowState state)
            where TWindow : Window<TWindowState>;

        Window GetCurrent();
    }
    
    // incoming
    public interface WindowManipulator
    {
        void SetVisible(Window window, bool visible);
    }
}