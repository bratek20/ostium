using B20.Frontend.Windows.Api;
using GameModule.ViewModels;

namespace GameWindowModule.ViewModels
{
    public partial class GameWindow : Window
    {
        public GameVm Game { get; set; }
    }
}