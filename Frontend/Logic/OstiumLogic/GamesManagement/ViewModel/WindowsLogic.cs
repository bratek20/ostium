using B20.Frontend.Windows.Api;
using GamesManagement.Api;
using User.Api;

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
            
        }
    }
}