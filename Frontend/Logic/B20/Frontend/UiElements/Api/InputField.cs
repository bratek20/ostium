using B20.Architecture.Logs.Api;

namespace B20.Frontend.UiElements
{
    public class InputField: UiElement<string>
    {
        public Logger Logger { get; set; }
        
        public void OnChange(string value)
        {
            Logger.Info($"Input field changed to `{value}`");
            Update(value);
        }
    }
}