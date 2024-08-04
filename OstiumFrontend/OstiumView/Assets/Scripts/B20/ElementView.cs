using UnityEngine;

namespace B20.View
{
    public class ElementView: MonoBehaviour
    {
        public void SetVisible(bool visible)
        {
            gameObject.SetActive(visible);
        }
    }
}