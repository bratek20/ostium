using B20.Frontend.UiElements;
using B20.Frontend.Windows.Api;
using B20.Logic;

namespace Main.ViewModel
{
    public partial class MainWindow : Window<EmptyWindowState>
    {
        public InputField Username { get; set; }
        public Button PlayButton { get; set; }
    }
}
