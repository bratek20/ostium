using B20.Frontend.Elements.View;
using SingleGame.Api;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
{
    public class TableView: ElementView<TableVm>
    {
        [SerializeField]
        private PlayerSideView mySide;
        [SerializeField]
        private PlayerSideView opponentSide;

        protected override void OnBind()
        {
            base.OnBind();
        
            mySide.Bind(ViewModel.MySide);
            opponentSide.Bind(ViewModel.OpponentSide);
        }
    }
}