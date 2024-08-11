using B20.Frontend.Postion;
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