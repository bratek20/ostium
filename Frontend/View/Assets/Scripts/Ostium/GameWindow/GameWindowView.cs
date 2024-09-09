using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class GameWindowView: WindowView<GameWindow>
    {
        [SerializeField]
        private GameView game;

        protected override void OnInit()
        {
            var gameVM = ViewModel.Game;
            game.Bind(gameVM);
        }
    }
}