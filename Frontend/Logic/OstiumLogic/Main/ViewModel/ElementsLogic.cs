﻿using B20.Frontend.Windows.Api;
using GameModule.ViewModel;

namespace Main.ViewModel
{
    public partial class PlayButton
    {
        private WindowManager windowManager;

        public PlayButton(WindowManager windowManager)
        {
            this.windowManager = windowManager;
        }

        protected override void OnClick()
        {
            windowManager.Open<GameWindow>();
        }
    }
}