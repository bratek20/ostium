using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class ElementView<T>: MonoBehaviour where T: ElementVM
    {
        public T Model { get; private set; }

        protected virtual void OnBind() { }

        public void Bind(T value)
        {
            Model = value;
            OnBind();
        }
        
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}