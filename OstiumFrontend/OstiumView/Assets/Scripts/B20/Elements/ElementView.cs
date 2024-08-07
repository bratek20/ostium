using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class ElementView<T>: MonoBehaviour where T: ElementVM
    {
        public T Model { get; private set; }

        protected virtual void OnBind() { }

        protected virtual void OnModelUpdate() { }

        public void Bind(T value)
        {
            Model = value;
            Model.SetUpdateObserver(OnModelUpdateObserver);
            OnBind();
        }

        private void OnModelUpdateObserver()
        {
            OnModelUpdate();
        }
        
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}