using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class CreateCardView: PanelView<CreatureCardVM>
    {
        [SerializeField]
        private LabelView name;

        protected override void OnBind()
        {
            base.OnBind();
            name.Bind(ViewModel.Name);
        }
    }
}