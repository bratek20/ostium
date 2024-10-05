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
        public enum Direction
        {
            Horizontal,
            Vertical
        }
        [SerializeField]
        protected TView elementPrefab;

        [SerializeField]
        private Direction direction = Direction.Horizontal;

        [SerializeField] 
        private float elementSpacing = 10;

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
        }
    }
}