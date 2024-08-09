using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class RowView: PanelView<RowVM>
    {
        [SerializeField]
        private CreateCardView card;

        protected override void OnBind()
        {
            base.OnBind();
            card.Bind(ViewModel.Card);
        }

        protected override void OnViewModelUpdate()
        {
            base.OnViewModelUpdate();
            card.SetVisible(ViewModel.Model.IsPresent());
        }
    }
}