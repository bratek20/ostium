using B20.Frontend.Elements.View;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
{
    public class HandView: ElementView<HandVm>
    {
        [SerializeField]
        private CardsListView cards;

        protected override void OnBind()
        {
            base.OnBind();
            cards.Bind(ViewModel.Cards);
        }
    }
}