using B20.Frontend.UiElements;
using B20.Types;
using UnityEngine;

namespace B20.Frontend.Traits.View
{
    public class WithPosition2dView: TraitView<WithPosition2d>
    {
        private RectTransform rectTransform;
        
        protected override void OnBind()
        {
            base.OnBind();
            rectTransform = gameObject.GetComponent<RectTransform>();
            Trait.SetPositionSetter(position =>
            {
                rectTransform.position = TypesConverter.Convert(position);
            });
        }
    }
}