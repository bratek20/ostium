using B20.Frontend.Element;
using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class OptionalElementView<TView, TViewModel, TModel>: 
        ElementView<OptionalElementVm<TViewModel, TModel>>
        where TView: ElementView<TViewModel>
        where TViewModel: ElementVm<TModel>
    {
        [SerializeField]
        private TView elementPrefab;
        
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