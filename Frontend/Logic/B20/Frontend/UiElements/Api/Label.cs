namespace B20.Frontend.UiElements
{
    public class Label: UiElement<string>
    {
        public void Update(int value)
        {
            Update(value.ToString());
        }
    }
    
    public class OptionalLabel: OptionalUiElement<Label, string>
    {
        public OptionalLabel(Label element) : base(element)
        {
        }
    }
}