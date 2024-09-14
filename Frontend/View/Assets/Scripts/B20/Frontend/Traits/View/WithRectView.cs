using B20.Frontend.Traits;
using B20.Types;
using UnityEngine;

namespace B20.Frontend.Traits.View
{
    public class WithRectView: TraitView<WithRect>
    {
        private RectTransform rectTransform;
        
        protected override void OnBind()
        {
            base.OnBind();
            rectTransform = gameObject.GetComponent<RectTransform>();
            Trait.RectProvider = () => TypesConverter.Convert(rectTransform);
            Debug.Log("WithRectView, rect: " + Trait.Rect + ", gameObject: " + gameObject);
        }
    }
}