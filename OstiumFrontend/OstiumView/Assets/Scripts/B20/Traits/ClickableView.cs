using B20.Frontend.Element;
using B20.Frontend.Traits;
using UnityEngine;
using UnityEngine.UI;

namespace B20.View
{
    public class ClickableView: TraitView
    {
        public void Bind(Clickable clickable)
        {
            var button = gameObject.AddComponent<Button>();
            button.onClick.AddListener(clickable.Click);
        }
    }
}