using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class ElementView<T>: MonoBehaviour where T: ElementVM
    {
        protected T ViewModel { get; private set; }

        protected virtual void OnBind() { }

        protected virtual void OnViewModelUpdate() { }

        public void Bind(T value)
        {
            ViewModel = value;
            ViewModel.SetObserverUpdateAction(OnObservedViewModelUpdate);
            OnBind();
        }

        private void OnObservedViewModelUpdate()
        {
            OnViewModelUpdate();
        }
        
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}