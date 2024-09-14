using System.Collections.Generic;
using B20.Frontend.Windows.Api;
using B20.View;
using UnityEngine;

namespace B20.Frontend.Windows.Integrations
{
    public class UnityWindowManipulator: MonoBehaviour, WindowManipulator
    {
        [SerializeField]    
        private List<WindowView> windowPrefabs;
        
        private List<WindowView> windowViews;

        private void InitWindowViews()
        {
            if (GetComponent<Canvas>() == null)
            {
                Debug.LogError("UnityWindowManipulator should be placed next to canvas");
            }

            windowViews = new List<WindowView>();
            windowPrefabs.ForEach(w =>
            {
                var view = Instantiate(w, transform);
                Debug.Log("Instantiated window view: " + view.GetType().Name);
                windowViews.Add(view);
            });
        }

        private WindowView EnsureInitializedAndGetView(Window viewModel)
        {
            if (windowViews == null)
            {
                InitWindowViews();
            }
            
            WindowView windowView = windowViews.Find(w => w.Accepts(viewModel));
            if (windowView == null)
            {
                Debug.LogError("Window view not found for view model: " + viewModel.GetType().Name);
                return null;
            }

            if (windowView.RawViewModel == null)
            {
                windowView.Init(viewModel);
            }
            return windowView;
        }

        public void SetVisible(Window window, bool visible)
        {
            Debug.Log("Setting window visibility: " + window.GetType().Name + " to " + visible);
            var view = EnsureInitializedAndGetView(window);
            view.SetActive(visible);
        }
    }
}