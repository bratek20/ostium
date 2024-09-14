using B20.Frontend.UiElements;
using UnityEngine;

namespace B20.Frontend.Traits.View
{
    public abstract class TraitView<T>: MonoBehaviour where T: Trait
    {
        public T Trait { get; private set;  }

        protected virtual void OnBind() { }

        public void Bind(T trait)
        {
            Trait = trait;
            OnBind();
        } 
    }
}