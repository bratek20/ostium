namespace B20.Frontend.Windows.Api
{
    public interface Window
    {
        
    } 
    
    public interface Window<T>: Window where T : WindowState
    {
        void OnOpen(T state) { }
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
            where TWindow : class, Window<TWindowState> 
            where TWindowState : WindowState;
        
        Window GetCurrent();
    }
    
    // incoming
    public interface WindowManipulator
    {
        void SetVisible(Window window, bool visible);
    }
}