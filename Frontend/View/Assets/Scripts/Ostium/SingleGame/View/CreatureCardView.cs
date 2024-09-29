using B20.Frontend.Elements.View;
using SingleGame.ViewModel;
using UnityEngine;

namespace SingleGame.View
{
    public class CreateCardView: ElementView<CreatureCardVm>
    {
        [SerializeField]
        private LabelView id;
        [SerializeField]
        private BoolSwitchView selected;

        protected override void OnBind()
        {
            base.OnBind();
            id.Bind(ViewModel.Id);
            selected.Bind(ViewModel.Selected);
        }
    }
}