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
            card.Bind(Model.Card);
        }

        protected override void OnModelUpdate()
        {
            base.OnModelUpdate();
            if (Model.Model != null)
            {
                card.SetVisible(true);
            }
            else
            {
                card.SetVisible(false);
            }
        }
    }
}