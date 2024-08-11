using System.Collections.Generic;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class ElementListView<TView, TViewModel, TModel>
        : ElementView<ElementListVm<TViewModel, TModel>> 
        where TView: ElementView<TViewModel>
        where TViewModel: ElementVm<TModel>
    {
        [SerializeField]
        private TView elementPrefab;

        [SerializeField]
        private int elementSpacing = 1;

        private readonly List<TView> _elementViews = new List<TView>();
        
        protected override void OnViewModelUpdate()
        {
            base.OnViewModelUpdate();
            
            Vector3 currentPosition = Vector3.zero;
            
            foreach (var view in _elementViews)
            {
                Destroy(view.gameObject);
            }
            _elementViews.Clear();
            
            foreach (var element in ViewModel.Elements)
            {
                var view = Instantiate(elementPrefab, transform);
                _elementViews.Add(view);
                
                view.transform.localPosition = currentPosition;
                view.Bind(element);
                element.Refresh();

                // Update the position for the next element
                currentPosition += new Vector3(elementSpacing, 0, 0);
            }
        }
    }
}