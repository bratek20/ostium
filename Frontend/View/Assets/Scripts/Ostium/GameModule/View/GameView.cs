using B20.Frontend.Elements.View;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
{
    public class GameView: ElementView<GameVm>
    {
        [SerializeField]
        private HandView hand;
        [SerializeField]
        private TableView table;
        
        protected override void OnBind()
        {
            hand.Bind(ViewModel.Hand);
            table.Bind(ViewModel.Table);
        }
    }
}