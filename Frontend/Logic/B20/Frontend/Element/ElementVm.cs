using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Api;
using B20.Architecture.Exceptions;

namespace B20.Frontend.Element
{
    public class TraitNotFoundException: ApiException
    {
        public TraitNotFoundException(string message) : base(message)
        {
        }
    }
    
    public interface ElementVm
    {
        void SetObserverUpdateAction(Action observerUpdateAction);
        
        void Refresh();
        
        
        List<Trait> GetTraits();
        T GetTrait<T>() where T: Trait;
    }

    public abstract class ElementVm<TModelType>: ElementVm
    {
        public Context Context { get; set; }
        
        public TModelType Model { get; private set; }
        
        private Action _observerUpdateAction;
        
        protected virtual void OnUpdate() { }
        
        protected virtual List<Type> GetTraitTypes()
        {
            return new List<Type>();
        }
        
        public void Update(TModelType model)
        {
            Model = model;
            
            OnUpdate();
            
            _observerUpdateAction?.Invoke();
        }

        public void SetObserverUpdateAction(Action observerUpdateAction)
        {
            this._observerUpdateAction = observerUpdateAction;
        }

        public void Refresh()
        {
            Update(Model);
        }

        private readonly List<Trait> _traits = new List<Trait>();
        
        // protected void AddTrait(Trait t)
        // {
        //     _traits.Add(t);
        //     t.Init(this);
        // }
        
        public T GetTrait<T>() where T: Trait
        {
            var result = _traits.Find(t => t is T) as T;
            if (result == null)
            {
                var message = $"Trait {typeof(T).Name} not found for {GetType().Name}";
                throw new TraitNotFoundException(message);
            }

            return result;
        }
        
        public List<Trait> GetTraits()
        {
            return _traits;
        }
    }
}