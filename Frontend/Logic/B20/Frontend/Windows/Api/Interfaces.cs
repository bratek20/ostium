namespace B20.Frontend.Windows.Api
{
    public interface Window
    {
        
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