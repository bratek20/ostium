using System;
using System.Collections.Generic;

namespace B20.Frontend.Element
{
    public interface ElementVM
    {
        void SetObserverUpdateAction(Action observerUpdateAction);
        
        void Refresh();
        
        List<Trait> GetTraits();
    }

    public abstract class ElementVM<ModelType>: ElementVM where ModelType: class
    {
        public ModelType Model { get; private set; }
        
        private Action observerUpdateAction;
        
        protected virtual void OnUpdate() { }
        
        public void Update(ModelType model)
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

        private List<Trait> traits = new List<Trait>();
        
        protected void AddTrait(Trait t)
        {
            traits.Add(t);
            t.Init(this);
        }
        
        protected T GetTrait<T>() where T: Trait
        {
            return traits.Find(t => t is T) as T;
        }
        
        public List<Trait> GetTraits()
        {
            return traits;
        }
    }
}