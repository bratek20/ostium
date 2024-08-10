using B20.Frontend.Traits;
using UnityEngine;
using UnityEngine.EventSystems;

namespace B20.View
{
    public class DraggableView : TraitView, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private Draggable draggable;
        private Transform parent;
        private Vector3 startPosition;

        public void Bind(Draggable draggable)
        {
            this.draggable = draggable;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag, eventData: " + eventData + ", element: " + gameObject);
            
            startPosition = transform.position;
            
            parent = transform.parent;
            transform.SetParent(transform.root);
            transform.SetAsLastSibling();
            
            draggable.StartDrag(new UnityPosition(transform.position));
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }
        
        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag, eventData: " + eventData + ", element: " + gameObject);
            
            transform.position = startPosition;
            
            transform.SetParent(parent);
            
            draggable.EndDrag(new UnityPosition(transform.position));
        }


    }
}