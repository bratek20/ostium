using System.Threading.Tasks;

namespace B20.Frontend.Elements
{
    public class ElementVM<T> where T: class
    {
        public T Model { get; private set; }
        
        protected virtual void OnUpdate() { }
        
        public void Update(T model)
        {
            Model = model;
            OnUpdate();
        }
    }
}