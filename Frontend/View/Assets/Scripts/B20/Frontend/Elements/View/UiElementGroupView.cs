using System;
using System.Collections.Generic;
using B20.Frontend.UiElements;
using UnityEngine;

namespace B20.Frontend.Elements.View
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }
    
    public class UiElementGroupView<TView, TViewModel, TModel>
        : ElementView<UiElementGroup<TViewModel, TModel>> 
        where TView: ElementView<TViewModel>
        where TViewModel: UiElement<TModel>
    {

        [SerializeField]
        protected TView elementPrefab;

        [SerializeField]
        protected Direction direction = Direction.Horizontal;

        [SerializeField] 
        protected float elementSpacing = 10;

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
            
            var elementSize = elementPrefab.GetComponent<RectTransform>().sizeDelta;
            int idx = 0;
            Vector3 elementSpacingVector = direction == Direction.Horizontal
                ? new Vector3(elementSpacing + elementSize.x, 0, 0)
                : new Vector3(0, elementSpacing + elementSize.y, 0);
            
            foreach (var element in ViewModel.Elements)
            {
                var view = Instantiate(elementPrefab, transform);
                view.name = elementPrefab.name + idx++;
                
                _elementViews.Add(view);
                
                view.transform.localPosition = currentPosition;
                view.Bind(element);
                element.Refresh();

                // Update the position for the next element
                currentPosition += elementSpacingVector;
            }
            
            var size = new Vector2(
                elementSize.x + elementSpacingVector.x * (ViewModel.Elements.Count - 1) + 50,
                elementSize.y + elementSpacingVector.y * (ViewModel.Elements.Count - 1) + 50
            );
            GetComponent<RectTransform>().sizeDelta = size;
        }
    }
}