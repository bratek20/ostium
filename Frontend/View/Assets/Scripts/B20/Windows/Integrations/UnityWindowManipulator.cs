using System.Collections.Generic;
using B20.Frontend.Windows.Api;
using B20.View;
using UnityEngine;

namespace B20.Frontend.Windows.Integrations
{
    public class UnityWindowManipulator: MonoBehaviour, WindowManipulator
    {
        [SerializeField]    
        private List<WindowView> windowViews;

        public void Init(WindowManager windowManager)
        {
            windowViews.ForEach(view =>
            {
                var viewModel = windowManager.GetAll().Find(w => view.Accepts(w));
                if (viewModel == null)
                {
                    Debug.LogError("Window view model not found for view: " + view.GetType().Name);
                }
                else
                {
                    view.Init(viewModel);
                    Debug.Log("Window view initialized: " + view.GetType().Name);
                }
            });
        }
        
        private WindowView EnsureInitializedAndGetView(Window viewModel)
        {
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