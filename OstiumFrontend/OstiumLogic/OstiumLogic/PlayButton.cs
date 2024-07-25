using B20.Logic;

namespace Ostium.Logic
{
    public class PlayButton : Button
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
