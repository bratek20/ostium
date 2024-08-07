using System.Collections.Generic;
using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class ElementListView<ViewType, ViewModelType>
        : ElementView<ElementListVM<ViewModelType>> 
        where ViewType: ElementView<ViewModelType>
        where ViewModelType: ElementVM
    {
        [SerializeField]
        private ViewType elementPrefab;

        [SerializeField]
        private int elementSpacing = 1;

        private List<ViewType> elementViews = new List<ViewType>();
        
        protected override void OnViewModelUpdate()
        {
            base.OnViewModelUpdate();
            
            Vector3 currentPosition = Vector3.zero;
            
            foreach (var view in elementViews)
            {
                Destroy(view.gameObject);
            }
            elementViews.Clear();
            
            foreach (var element in ViewModel.Model)
            {
                var view = Instantiate(elementPrefab, transform);
                elementViews.Add(view);
                
                view.transform.localPosition = currentPosition;
                view.Bind(element);
                element.Refresh();

                // Update the position for the next element
                currentPosition += new Vector3(elementSpacing, 0, 0);
            }
        }
    }
}