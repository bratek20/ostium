using System;
using System.Collections.Generic;
using System.Linq;
using B20.Architecture.Exceptions;
using B20.Frontend.Traits;

namespace B20.Frontend.UiElements
{
    public class TraitNotFoundException: ApiException
    {
        public TraitNotFoundException(string message) : base(message)
        {
        }
    }
    
    public interface UiElement
    {
        void SetObserverUpdateAction(Action observerUpdateAction);
        
        void Refresh();
        
        List<Trait> Traits { get; }
        
        T GetTrait<T>() where T: Trait;
        
        List<UiElement> Children { get; }
        
        List<UiElement> Descendants
        {
            get
            {
                var descendants = new List<UiElement>();
                foreach (var child in Children)
                {
                    descendants.Add(child);
                    descendants.AddRange(child.Descendants);
                }
                return descendants;
            }
        }
    }

    public class UiElementHelper
    {
        public static List<UiElement> GetElementProperties(Object obj)
        {
            var fields = obj.GetType().GetProperties()
                .Where(f => typeof(UiElement).IsAssignableFrom(f.PropertyType));
            return fields.Select(f => f.GetValue(obj) as UiElement).ToList();
        }
    }

    public class EmptyModel { }

    public abstract class UiElement<TModelType>: UiElement
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
                    var finalTraitTypes = GetTraitTypes();
                    //TODO-REF should be more generic
                    if (finalTraitTypes.Contains(typeof(Draggable)) && !finalTraitTypes.Contains(typeof(WithPosition2d)))
                    {
                        finalTraitTypes.Add(typeof(WithPosition2d));
                    }
                    
                    _traits = finalTraitTypes.Select(type => TraitFactory.Create(type)).ToList();
                    _traits.ForEach(trait => trait.Init(this));
                }

                return _traits;
            }
        }

        private bool _started = false;
        protected virtual void OnStart() { }

        protected virtual void OnUpdate() { }
        
        protected virtual List<Type> GetTraitTypes()
        {
            return new List<Type>();
        }

        public void Update(TModelType model)
        {
            Model = model;
            if (!_started)
            {
                OnStart();
                _started = true;
            }
            
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

        private List<UiElement> children = null;
        public List<UiElement> Children {
            get {
                if (children == null) {
                    children = UiElementHelper.GetElementProperties(this);
                }
                return children;
            }
        }
    }
}