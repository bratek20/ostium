using System.Linq.Expressions;

namespace B20.Frontend.Windows.Api
{
    public interface Window
    {
        void OnOpen() {}
    }
    
    // outgoing
    public interface WindowManager
    {
        T Get<T>() where T : class, Window;
        
        void Open<T>() where T : class, Window;
        
        T GetCurrent<T>() where T : class, Window;
    }
    
    // incoming
    public interface WindowManipulator
    {
        void SetVisible(Window window, bool visible);
    }
}