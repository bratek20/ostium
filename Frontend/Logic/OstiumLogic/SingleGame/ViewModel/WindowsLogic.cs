using B20.Frontend.Windows.Api;

namespace SingleGame.ViewModel
{
    public partial class GameWindow
    {
        public void OnOpen(EmptyWindowState s)
        {
            Game.UpdateState();
        }
    }
}