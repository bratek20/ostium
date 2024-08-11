using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class VisibleView: ElementView<VisibleVm>
    {
        protected override void OnViewModelUpdate()
        {
            Debug.Log("Setting visible: " + ViewModel.Model + " for " + gameObject.name);
            gameObject.SetActive(ViewModel.Model);
        }
    }
}