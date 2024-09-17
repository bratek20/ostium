using GamesManagement.Api;

namespace GamesManagement.ViewModel
{
    public partial class GamesManagementWindow
    {
        private GamesManagementApi api;

        public GamesManagementWindow(GamesManagementApi api)
        {
            this.api = api;
        }

        protected override void OnOpen()
        {
            CreateGame.OnClick(OnCreateClicked);
            
            CreatedGames.Update(api.GetAllCreated());
        }
        
        private void OnCreateClicked()
        {
            api.Create(State.Username);
        }
    }
}