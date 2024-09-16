using B20.Frontend.Windows.Api;

namespace SingleGame.ViewModel
{
    public partial class GameWindow
    {
        protected override void OnOpen()
        {
            Game.UpdateState();
        }
    }
}