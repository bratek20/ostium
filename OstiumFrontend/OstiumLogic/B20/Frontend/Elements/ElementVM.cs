using System;

namespace B20.Frontend.Elements
{
    public interface ElementVM
    {
        void SetUpdateObserver(Action onUpdate);
    }

    public class ElementVM<T>: ElementVM where T: class
    {
        public T Model { get; private set; }
        
        private Action onUpdate;
        
        protected virtual void OnUpdate() { }
        
        public void Update(T model)
        {
            Model = model;
            
            OnUpdate();
            
            if (onUpdate == null)
            {
                //throw new Exception("Update observer not set for object of type " + GetType());
            }
            onUpdate?.Invoke();
        }

        public void SetUpdateObserver(Action onUpdate)
        {
            this.onUpdate = onUpdate;
        }
    }
}