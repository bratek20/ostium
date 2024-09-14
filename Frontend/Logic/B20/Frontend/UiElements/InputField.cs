namespace B20.Frontend.UiElements
{
    public class InputField: UiElement<string>
    {
        public void OnChange(string value)
        {
            Update(value);
        }
    }
}