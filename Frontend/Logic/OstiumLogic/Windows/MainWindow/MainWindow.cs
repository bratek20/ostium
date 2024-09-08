using B20.Frontend.Windows.Api;
using B20.Logic;

namespace Ostium.Logic
{
    public class MainWindow : Window
    {
        public PlayButton PlayButton { get; }
        
        public MainWindow(PlayButton playButton)
        {
            PlayButton = playButton;
        }
        
        public WindowId GetId()
        {
            return WindowIds.MAIN_WINDOW;
        }
    }
}
