using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class GameView: ElementView<GameVM>
    {
        [SerializeField]
        private HandView hand;
        [SerializeField]
        private TableView table;
        
        protected override void OnBind()
        {
            hand.Bind(Model.Hand);
            table.Bind(Model.Table);
        }
    }
}