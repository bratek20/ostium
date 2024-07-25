using Logic.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ostium.Logic
{
    class PlayButton : Button
    {
        private GameState state;
        private GameWindow gameWindow;

        public PlayButton(GameState state, GameWindow gameWindow)
        {
            this.state = state;
            this.gameWindow = gameWindow;
        }

        protected override void OnClick()
        {
            state.ChangeWindow(gameWindow);
        }
    }
}
