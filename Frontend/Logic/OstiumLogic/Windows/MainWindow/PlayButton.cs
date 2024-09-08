using System;
using B20.Frontend.Windows.Api;
using B20.Logic;

namespace Ostium.Logic
{
    public class PlayButton : Button
    {
        //TODO-REF ugly solution
        public Lazy<WindowManager> WindowManager { get; set; }

        protected override void OnClick()
        {
            WindowManager.Value.Open<GameWindow>();
        }
    }
}
