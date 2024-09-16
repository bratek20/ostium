using B20.Frontend.Windows.Api;
using B20.Logic;
using User.Api;

namespace GamesManagement.ViewModel
{
    public partial class GamesManagementWindow : Window<GamesManagementWindow.State>
    {
        public class State
        {
            public Username Username { get; }
            
            public State(Username username)
            {
                Username = username;
            }
        }
        public CreatedGameVmGroup CreatedGames { get; set; }
        public Button CreateGame { get; set; }
    }
}
