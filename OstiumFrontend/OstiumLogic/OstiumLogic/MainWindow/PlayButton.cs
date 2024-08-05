using B20.Frontend.Windows.Api;
using B20.Logic;

namespace Ostium.Logic
{
    public class PlayButton : Button
    {
        private WindowManager windowManager;

        public PlayButton(WindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        protected override void OnClick()
        {
            windowManager.Open(WindowIds.GAME_WINDOW);
        }
    }
}
