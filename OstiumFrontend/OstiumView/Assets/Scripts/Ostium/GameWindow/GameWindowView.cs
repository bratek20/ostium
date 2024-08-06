using B20.View;
using GameSetup.Api;
using UnityEngine;

namespace Ostium.View
{
    public class GameWindowView: WindowView
    {
        [SerializeField]
        private HandView hand;
        [SerializeField]
        private TableView table;
        
        public void Refresh(Game game)
        {
            hand.Init(game.GetHand());
            table.Init(game.GetTable());
        }
    }
}