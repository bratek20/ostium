using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class GameWindowView: WindowView
    {
        [SerializeField]
        private HandView hand;
        [SerializeField]
        private TableView table;

        protected override void OnInit()
        {
            var game = ValueAs<GameWindow>().Game;
            hand.Init(game.GetHand());
            table.Init(game.GetTable());
        }
    }
}