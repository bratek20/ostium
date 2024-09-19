using B20.Frontend.Elements.View;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
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