using B20.View;
using GameModule.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace GameModule.View
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