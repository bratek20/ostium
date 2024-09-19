using B20.Architecture.Logs.Api;

namespace B20.Frontend.UiElements
{
    public class InputField: UiElement<string>
    {
        public Logger Logger { get; set; }
        
        public void OnTextChange(string text)
        {
            Logger.Info($"Input field text changed to `{text}`");
            Update(text);
        }
    }
}