using B20.Frontend.Elements.View;
using SingleGame.ViewModel;
using Ostium.Logic;
using UnityEngine;
using UnityEngine.Serialization;

namespace SingleGame.View
{
    public class GameWindowView: WindowView<GameWindow>
    {
        [SerializeField]
        private GameStateView gameState;

        protected override void OnInit()
        {
            gameState.Bind(ViewModel.GameState);
        }
    }
}