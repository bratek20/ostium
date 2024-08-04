using B20.Logic;
using TMPro;
using UnityEngine;

namespace B20.View
{
    public class LabelView : MonoBehaviour
    {
        private TextMeshProUGUI text;

        public void Init(string value)
        {
            text = GetComponent<TextMeshProUGUI>();
            text.text = value;
        }
    }
}

