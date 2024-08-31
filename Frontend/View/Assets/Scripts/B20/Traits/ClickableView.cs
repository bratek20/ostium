using B20.Frontend.Traits;
using UnityEngine.UI;

namespace B20.View
{
    public class ClickableView: TraitView<Clickable>
    {
        protected override void OnBind()
        {
            base.OnBind();
            var button = gameObject.AddComponent<Button>();
            button.onClick.AddListener(Trait.Click);
        }
    }
}