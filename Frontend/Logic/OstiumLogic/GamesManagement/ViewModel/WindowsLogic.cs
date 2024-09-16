using B20.Frontend.Windows.Api;
using User.Api;

namespace GamesManagement.ViewModel
{
    public partial class GamesManagementWindow
    {
        private WindowManager windowManager;

        public GamesManagementWindow(WindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        protected override void OnOpen()
        {
            
        }
    }
}