using B20.Frontend.Elements.View;
using GamesManagement.ViewModel;
using Main.ViewModel;
using UnityEngine;

namespace GamesManagement.View
{
    public class GamesManagementWindowView: WindowView<GamesManagementWindow>
    {
        [SerializeField] 
        private CreatedGameGroupView createdGames;
        [SerializeField] 
        private ButtonView createGame;

        protected override void OnInit()
        {
            createdGames.Bind(ViewModel.CreatedGames);
            createGame.Bind(ViewModel.CreateGame);
        }
    }
}