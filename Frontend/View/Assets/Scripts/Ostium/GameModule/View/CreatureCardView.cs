using B20.Frontend.Elements.View;
using GameModule.ViewModel;
using UnityEngine;

namespace GameModule.View
{
    public class CreateCardView: ElementView<CreatureCardVm>
    {
        [SerializeField]
        private LabelView name;
        //TODO-REF selected should be image below parent
        [SerializeField]
        private BoolSwitchView selected;

        protected override void OnBind()
        {
            base.OnBind();
            name.Bind(ViewModel.Name);
            selected.Bind(ViewModel.Selected);
        }
    }
}