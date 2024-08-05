namespace B20.Frontend.Windows.Api
{
    public interface WindowManager
    {
        void Open(WindowId id);

        WindowId GetCurrent();
    }
}