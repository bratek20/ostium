using B20.Frontend.Windows.Api;
using B20.Logic;

namespace Ostium.Logic
{
    public class MainWindow : Window
    {
        public PlayButton PlayButton { get; private set;  }
        
        public MainWindow(WindowManager windowManager)
        {
            PlayButton = new PlayButton(windowManager);
        }
        
        public WindowId GetId()
        {
            return WindowIds.MAIN_WINDOW;
        }
    }
}
