using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using UnityEngine;

namespace B20.View
{
    public class ElementView<T>: MonoBehaviour where T: ElementVm
    {
        public T ViewModel { get; private set; }

        protected virtual void OnBind() { }

        protected virtual void OnViewModelUpdate() { }

        public void Bind(T value)
        {
            ViewModel = value;
            ViewModel.SetObserverUpdateAction(OnObservedViewModelUpdate);
            
            ViewModel.Traits.ForEach(BindTrait);
            
            OnBind();
        }

        //TODO-REF make it generic with some global trait dictionary
        private void BindTrait(Trait t)
        {
            if (t is Clickable c)
            {
                gameObject.AddComponent<ClickableView>().Bind(c);
            }
            if (t is Draggable d)
            {
                gameObject.AddComponent<DraggableView>().Bind(d);
            }
            if (t is WithRect r)
            {
                gameObject.AddComponent<WithRectView>().Bind(r);
            }
            if (t is WithPosition2d p)
            {
                gameObject.AddComponent<WithPosition2dView>().Bind(p);
            }
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