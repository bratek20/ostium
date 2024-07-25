using Logic;
using Logic.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View
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

