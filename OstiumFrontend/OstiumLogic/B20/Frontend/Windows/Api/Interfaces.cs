namespace B20.Frontend.Windows.Api
{
    public interface Window
    {
        WindowId GetId();    
    }
    
    // outgoing
    public interface WindowManager
    {
        void Register(Window window);
        
        void Open(WindowId id);

        WindowId GetCurrent();
    }
    
    // incoming
    public interface WindowManipulator
    {
        void SetVisible(WindowId id, bool visible);
    }
}