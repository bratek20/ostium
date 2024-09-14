using B20.View;
using GameModule.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace GameModule.View
{
    public class RowView: ElementView<RowVm>
    {
        [SerializeField]
        private OptionalCreateCardView card;

        protected override void OnBind()
        {
            base.OnBind();
            card.Bind(ViewModel.Card);
        }
    }
}