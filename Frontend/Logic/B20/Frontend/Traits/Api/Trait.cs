namespace B20.Frontend.UiElements
{
    public class Trait
    {
        protected UiElement Owner { get; private set; }
        
        public void Init(UiElement owner)
        {
            Owner = owner;
        }
    }
}