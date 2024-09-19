using B20.Frontend.Windows.Api;
using GamesManagement.Api;
using SingleGame.ViewModel;
using User.Api;

namespace SingleGame.ViewModel
{
    public class GameWindowState
    {
        public Username User { get; }
        public GameId GameId { get; }

        public GameWindowState(Username user, GameId gameId)
        {
            User = user;
            GameId = gameId;
        }
    }
    
    public partial class GameWindow : Window<GameWindowState>
    {
        public GameVm Game { get; set; }
    }
}