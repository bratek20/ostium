using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class RowView: ElementView<RowVM>
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