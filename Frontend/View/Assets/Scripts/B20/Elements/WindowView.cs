using System;
using B20.Frontend.Windows.Api;
using B20.Logic;
using UnityEngine;

namespace B20.View
{
    public interface WindowView 
    {
        Window RawViewModel { get; }
        void SetActive(bool active);
    }
    
    public class WindowView<T>: MonoBehaviour, WindowView where T : Window  
    {
        public T ViewModel { get; private set; }
        public Window RawViewModel => ViewModel;
        
        protected virtual void OnInit() { }
        
        public void Init(T value)
        {
            ViewModel = value;
            OnInit();
        }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    }
}

