using B20.Frontend.Traits;
using B20.Types;
using UnityEngine;
using UnityEngine.EventSystems;

namespace B20.View
{
    public class DraggableView : TraitView<Draggable>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform parent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            parent = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            
            var position = TypesConverter.Convert(transform.position);
            Debug.Log("OnBeginDrag, position: " + position);
            Trait.StartDrag(TypesConverter.Convert(transform.position));
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            transform.SetParent(parent);
            
            var position = TypesConverter.Convert(transform.position);
            Debug.Log("OnEndDrag, position: " + position);
            Trait.EndDrag(position);
        }
    }
}