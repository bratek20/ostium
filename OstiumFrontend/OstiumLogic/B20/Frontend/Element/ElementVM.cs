using System;

namespace B20.Frontend.Element
{
    public interface ElementVM
    {
        void SetObserverUpdateAction(Action observerUpdateAction);
        
        void Refresh();
    }

    public class ElementVM<T>: ElementVM where T: class
    {
        public T Model { get; private set; }
        
        private Action observerUpdateAction;
        
        protected virtual void OnUpdate() { }
        
        public void Update(T model)
        {
            Model = model;
            
            OnUpdate();
            
            observerUpdateAction?.Invoke();
        }

        public void SetObserverUpdateAction(Action observerUpdateAction)
        {
            this.observerUpdateAction = observerUpdateAction;
        }

        public void Refresh()
        {
            Update(Model);
        }
    }
}