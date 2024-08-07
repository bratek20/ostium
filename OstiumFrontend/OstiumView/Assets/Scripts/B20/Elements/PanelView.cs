using B20.Frontend.Elements;
using UnityEngine.UI;

namespace B20.View
{
    public class PanelView<T> : ElementView<T> where T : PanelVM
    {
        protected override void OnBind()
        {
            if (Model.Clickable)
            {
                var button = gameObject.AddComponent<Button>();
                button.onClick.AddListener(OnClick);
            }
        }

        private void OnClick()
        {
            Model.Click();
        }
    }
}