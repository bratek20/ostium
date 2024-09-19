using B20.Frontend.Elements.View;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
{
    public class GameView: ElementView<GameVm>
    {
        [SerializeField]
        private HandView myHand;
        [SerializeField]
        private TableView table;
        // [SerializeField]
        // private HandView opponentHand;
        
        protected override void OnBind()
        {
            myHand.Bind(ViewModel.MyHand);
            table.Bind(ViewModel.Table);
            //opponentHand.Bind(ViewModel.OpponentHand);
        }
    }
}