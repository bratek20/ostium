using Logic;
using Logic.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
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

