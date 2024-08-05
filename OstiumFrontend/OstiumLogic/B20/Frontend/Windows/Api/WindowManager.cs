namespace B20.Frontend.Windows.Api
{
    public interface WindowManager
    {
        void Register(WindowId id);
        
        void Open(WindowId id);

        WindowId GetCurrent();
    }
}