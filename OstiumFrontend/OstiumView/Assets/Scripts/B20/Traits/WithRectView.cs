using B20.Frontend.Traits;
using B20.Types;
using UnityEngine;

namespace B20.View
{
    public class WithRectView: TraitView<WithRect>
    {
        protected override void OnBind()
        {
            base.OnBind();
            var rectTransform = gameObject.GetComponent<RectTransform>();
            Trait.Rect = TypesConverter.Convert(rectTransform);
        }
    }
}