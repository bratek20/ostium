namespace B20.Frontend.Element
{
    public interface PanelVM: ElementVM
    {
    }

    public class PanelVM<T>: ElementVM<T>, PanelVM where T: class
    {
    }
}