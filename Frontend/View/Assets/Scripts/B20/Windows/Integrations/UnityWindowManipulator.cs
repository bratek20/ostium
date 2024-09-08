using System.Collections.Generic;
using System.Linq;
using B20.Frontend.Windows.Api;
using B20.View;
using UnityEngine;

namespace B20.Frontend.Windows.Integrations
{
    public class UnityWindowManipulator: WindowManipulator
    {
        private List<WindowView> windowViews;
        public UnityWindowManipulator(IEnumerable<WindowView> windowViews)
        {
            this.windowViews = windowViews.ToList();
        }
        
        public void SetVisible(Window window, bool visible)
        {
            Debug.Log("Setting window visibility: " + window.GetType().Name + " to " + visible);
            WindowView windowView = windowViews.Find(w => w.RawViewModel == window);
            if (windowView == null)
            {
                Debug.LogError("Window view not found for window: " + window.GetType().Name);
                return;
            }
            windowView.SetActive(visible);
        }
    }
}