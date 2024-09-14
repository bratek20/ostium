using B20.Frontend.Elements;
using B20.Types;
using UnityEngine;

namespace B20.View
{
    public class WithPosition2dView: TraitView<WithPosition2d>
    {
        private RectTransform rectTransform;
        
        protected override void OnBind()
        {
            base.OnBind();
            rectTransform = gameObject.GetComponent<RectTransform>();
            Trait.PositionSetter = position =>
            {
                rectTransform.position = TypesConverter.Convert(position);
            };
        }
    }
}