using B20.Frontend.Windows.Api;
using UnityEngine;

namespace B20.View
{
    public abstract class WindowView: MonoBehaviour 
    {
        public abstract bool Accepts(Window window);
        
        public Window RawViewModel { get; private set; }
        
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
        
        protected virtual void OnInit() { }

        public void Init(Window viewModel)
        {
            RawViewModel = viewModel;
            OnInit();
        }
    }
    
    public class WindowView<T>: WindowView where T : Window  
    {
        public T ViewModel => (T) RawViewModel;
        
        public override bool Accepts(Window window)
        {
            return window is T;
        }
    }
}

