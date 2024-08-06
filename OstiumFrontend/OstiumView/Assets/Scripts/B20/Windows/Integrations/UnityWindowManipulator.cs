using System.Collections.Generic;
using B20.Frontend.Windows.Api;
using B20.View;
using UnityEngine;

namespace B20.Frontend.Windows.Integrations
{
    public class UnityWindowManipulator: WindowManipulator
    {
        private List<WindowView> windowViews;
        
        public UnityWindowManipulator(List<WindowView> windowViews)
        {
            this.windowViews = windowViews;
        }
        
        public void SetVisible(WindowId id, bool visible)
        {
            Debug.Log("Setting window visibility: " + id.Value + " to " + visible);
            WindowView windowView = windowViews.Find(w => w.Value.GetId().Value == id.Value);
            windowView.SetActive(visible);
        }
    }
}