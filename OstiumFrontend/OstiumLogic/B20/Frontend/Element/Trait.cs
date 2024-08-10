namespace B20.Frontend.Element
{
    public class Trait
    {
        protected ElementVM Owner { get; private set; }
        
        public void Init(ElementVM owner)
        {
            Owner = owner;
        }
    }
}