using B20.Frontend.Windows.Api;
using User.Api;

namespace Main.ViewModel
{
    public partial class MainWindow
    {
        private WindowManager windowManager;

        public MainWindow(WindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        protected override void OnOpen()
        {
            PlayButton.OnClick(OnPlayButtonClicked);
        }
        
        private void OnPlayButtonClicked()
        {
            var username = Username.Model;
            windowManager.Open<GamesManagementWindow, GamesManagementWindow.State>(
                new GamesManagementWindow.State(new Username(username))
            );
        }
    }
}