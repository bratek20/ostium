using B20.Frontend.Traits;
using B20.Types;
using UnityEngine;
using UnityEngine.EventSystems;

namespace B20.View
{
    public class DraggableView : TraitView<Draggable>, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Transform _parent;

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parent = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            
            var position = TypesConverter.Convert(transform.position);
            Debug.Log("OnBeginDrag, position: " + position);
            Trait.StartDrag(position);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            var position = TypesConverter.Convert(Input.mousePosition);
            Debug.Log("OnDrag, position: " + position);
            Trait.OnDrag(position);
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            var position = TypesConverter.Convert(transform.position);
            Debug.Log("OnEndDrag, position: " + position);
            Trait.EndDrag(position);
            
            transform.SetParent(_parent);

        }
    }
}