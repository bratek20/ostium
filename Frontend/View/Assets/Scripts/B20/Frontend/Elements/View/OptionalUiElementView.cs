using B20.Frontend.UiElements;
using UnityEngine;

namespace B20.Frontend.Elements.View
{
    public class OptionalUiElementView<TView, TViewModel, TModel>: 
        ElementView<OptionalUiElement<TViewModel, TModel>>
        where TView: ElementView<TViewModel>
        where TViewModel: UiElement<TModel>
    {
        [SerializeField]
        protected TView elementPrefab;
        
        private TView _elementView;

        protected override void OnBind()
        {
            base.OnBind();
            _elementView = Instantiate(elementPrefab, transform);
            _elementView.Bind(ViewModel.Element);
        }

        protected override void OnViewModelUpdate()
        {
            base.OnViewModelUpdate();
            _elementView.SetVisible(ViewModel.Model.IsPresent());
        }
    }
}