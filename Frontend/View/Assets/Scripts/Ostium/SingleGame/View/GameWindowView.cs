using B20.Frontend.Elements.View;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
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