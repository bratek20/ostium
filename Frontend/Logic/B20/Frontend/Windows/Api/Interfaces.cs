namespace B20.Frontend.Windows.Api
{
    public interface Window
    {
        
    } 
    
    public abstract class Window<T>: Window where T : WindowState
    {
        protected T State { get; private set; }
        public void Open(T state)
        {
            State = state;
            OnOpen();
        }
        
        protected virtual void OnOpen() { }
    }

    public interface WindowState
    {
    }
    
    public class EmptyWindowState : WindowState
    {
    }

    // outgoing
    public interface WindowManager
    {
        T Get<T>() where T : class, Window;

        void Open<TWindow, TWindowState>(TWindowState state) 
            where TWindow : Window<TWindowState> 
            where TWindowState : WindowState;
        
        Window GetCurrent();
    }
    
    // incoming
    public interface WindowManipulator
    {
        void SetVisible(Window window, bool visible);
    }
}