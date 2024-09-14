namespace B20.Frontend.Element
{
    public class Trait
    {
        protected ElementVm Owner { get; private set; }
        
        public void Init(ElementVm owner)
        {
            Owner = owner;
        }
    }
}