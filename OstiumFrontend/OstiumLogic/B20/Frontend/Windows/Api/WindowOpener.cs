namespace B20.Frontend.Windows.Api
{
    //TODO-REF WindowOpener -> WindowManipulator with SetVisible
    public interface WindowOpener
    {
        void Open(WindowId id);
    }
}