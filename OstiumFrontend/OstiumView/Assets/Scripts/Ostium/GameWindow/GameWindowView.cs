using B20.View;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class GameWindowView: WindowView
    {
        [SerializeField]
        private GameView game;

        protected override void OnInit()
        {
            var gameVM = ValueAs<GameWindow>().Game;
            game.Bind(gameVM);
        }
    }
}