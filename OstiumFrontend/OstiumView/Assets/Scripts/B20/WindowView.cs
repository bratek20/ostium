using B20.Logic;
using UnityEngine;

namespace B20.View
{
    public class WindowView : MonoBehaviour
    {
        public Window Value { get; private set; }

        public void Init(Window value)
        {
            Value = value;
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}
