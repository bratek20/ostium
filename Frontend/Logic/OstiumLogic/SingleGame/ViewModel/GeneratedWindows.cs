using B20.Frontend.Windows.Api;
using SingleGame.ViewModel;

namespace SingleGame.ViewModel
{
    public partial class GameWindow : Window<EmptyWindowState>
    {
        public GameVm Game { get; set; }
    }
}