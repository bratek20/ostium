using System.Collections.Generic;
using B20.Frontend.UiElements;
using UnityEngine;

namespace B20.Frontend.Elements.View
{
    public class UiElementGroupView<TView, TViewModel, TModel>
        : ElementView<UiElementGroup<TViewModel, TModel>> 
        where TView: ElementView<TViewModel>
        where TViewModel: UiElement<TModel>
    {
        [SerializeField]
        private TView elementPrefab;

        [SerializeField]
        private int elementSpacingX = 0;
        [SerializeField]
        private int elementSpacingY = 0;

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
            
            int idx = 0;
            foreach (var element in ViewModel.Elements)
            {
                var view = Instantiate(elementPrefab, transform);
                view.name = elementPrefab.name + idx++;
                
                _elementViews.Add(view);
                
                view.transform.localPosition = currentPosition;
                view.Bind(element);
                element.Refresh();

                // Update the position for the next element
                currentPosition += new Vector3(elementSpacingX, elementSpacingY, 0);
            }
        }
    }
}