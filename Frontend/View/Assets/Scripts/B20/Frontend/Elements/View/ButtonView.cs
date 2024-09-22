using B20.Frontend.UiElements;
using UnityEngine;

namespace B20.Frontend.Elements.View
{
    public class ButtonView : MonoBehaviour
    {
        public Button Value { get; private set; }

        private UnityEngine.UI.Button uiButton;

        public void Bind(Button value)
        {
            Value = value;
            uiButton = GetComponent<UnityEngine.UI.Button>();

            if (uiButton != null)
            {
                uiButton.onClick.AddListener(OnButtonClick);
            }
            else
            {
                Debug.LogError("ButtonView: No Button component found on the GameObject.");
            }
        }

        private void OnButtonClick()
        {
            Value.Click();
        }
    }
}

