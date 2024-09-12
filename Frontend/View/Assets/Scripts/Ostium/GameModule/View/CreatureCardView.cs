using B20.View;
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
        private VisibleView selected;
        [SerializeField]
        private Position2dView position;

        protected override void OnBind()
        {
            base.OnBind();
            name.Bind(ViewModel.Name);
            selected.Bind(ViewModel.Selected);
            position.Bind(ViewModel.Position);
        }
    }
}