using B20.View;
using GameModule.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace GameModule.View
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