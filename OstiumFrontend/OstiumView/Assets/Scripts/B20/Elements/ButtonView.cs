using B20.Logic;
using UnityEngine;

namespace B20.View
{
    public class ButtonView : MonoBehaviour
    {
        public Button Value { get; private set; }

        private UnityEngine.UI.Button uiButton;

        public void Init(Button value)
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
