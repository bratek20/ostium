using B20.Frontend.Elements;
using B20.Frontend.UiElements;
using UnityEngine;

namespace B20.Frontend.Elements.View
{
    public class BoolSwitchView: ElementView<BoolSwitch>
    {
        protected override void OnViewModelUpdate()
        {
            Debug.Log("Setting visible: " + ViewModel.Model + " for " + gameObject.name);
            gameObject.SetActive(ViewModel.Model);
        }
    }
}