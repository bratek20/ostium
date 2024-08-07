using System.Linq.Expressions;

namespace B20.Frontend.Windows.Api
{
    public interface Window
    {
        WindowId GetId();

        void OnOpen();
    }
    
    // outgoing
    public interface WindowManager
    {
        void Register(Window window);
        Window Get(WindowId id);
        
        void Open(WindowId id);
        WindowId GetCurrent();
    }
    
    // incoming
    public interface WindowManipulator
    {
        void SetVisible(WindowId id, bool visible);
    }
}