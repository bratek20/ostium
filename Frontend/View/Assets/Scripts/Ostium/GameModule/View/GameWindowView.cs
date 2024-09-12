using B20.View;
using GameModule.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace GameModule.View
{
    public class GameWindowView: WindowView<GameWindow>
    {
        [SerializeField]
        private GameView game;

        protected override void OnInit()
        {
            game.Bind(ViewModel.Game);
        }
    }
}