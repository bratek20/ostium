using System;
using System.Collections.Generic;
using System.Linq;
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
        
        
        List<Trait> Traits { get; }
        
        T GetTrait<T>() where T: Trait;
    }

    public abstract class ElementVm<TModelType>: ElementVm
    {
        public TraitFactory TraitFactory { get; set; }
        
        public TModelType Model { get; private set; }
        
        private Action _observerUpdateAction;
        private List<Trait> _traits;
        
        public List<Trait> Traits
        {
            get
            {
                if (_traits == null)
                {
                    _traits = GetTraitTypes().Select(type => TraitFactory.Create(type)).ToList();
                    _traits.ForEach(trait => trait.Init(this));
                }

                return _traits;
            }
        }

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

        public T GetTrait<T>() where T: Trait
        {
            var result = Traits.Find(t => t is T) as T;
            if (result == null)
            {
                var message = $"Trait {typeof(T).Name} not found for {GetType().Name}";
                throw new TraitNotFoundException(message);
            }

            return result;
        }
    }
}