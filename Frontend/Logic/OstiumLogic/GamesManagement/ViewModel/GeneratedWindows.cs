using B20.Frontend.Windows.Api;
using B20.Logic;
using User.Api;

namespace GamesManagement.ViewModel
{
    public class GamesManagementWindowState
    {
        public Username Username { get; }
            
        public GamesManagementWindowState(Username username)
        {
            Username = username;
        }
    }
    
    public partial class GamesManagementWindow : Window<GamesManagementWindowState>
    {
        public CreatedGameVmGroup CreatedGames { get; set; }
        public Button CreateGame { get; set; }
    }
}
