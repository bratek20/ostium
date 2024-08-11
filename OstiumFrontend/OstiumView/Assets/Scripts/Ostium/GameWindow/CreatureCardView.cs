using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class CreateCardView: ElementView<CreatureCardVm>
    {
        [SerializeField]
        private LabelView name;
        [SerializeField]
        private VisibleView selected;

        protected override void OnBind()
        {
            base.OnBind();
            name.Bind(ViewModel.Name);
            selected.Bind(ViewModel.Selected);
        }
    }
}