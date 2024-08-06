using System;
using B20.Frontend.Windows.Api;
using B20.Logic;
using UnityEngine;

namespace B20.View
{
    public class WindowView : MonoBehaviour
    {
        public Window Value { get; private set; }
        
        public T ValueAs<T>() where T : class, Window
        {
            return Value as T;
        }
        
        protected virtual void OnInit() { }
        
        public void Init(Window value)
        {
            Value = value;
            OnInit();
        }

        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}

