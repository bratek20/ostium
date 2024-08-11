using B20.Frontend.Elements;
using UnityEngine;

namespace B20.View
{
    public class VisibleView: ElementView<VisibleVm>
    {
        private GameObject _go;

        protected override void OnBind()
        {
            _go = gameObject;
        }

        protected override void OnViewModelUpdate()
        {
            _go.SetActive(ViewModel.Model);
        }
    }
}