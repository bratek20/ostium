namespace B20.Logic
{
    public class GameState
    {
        private GameStateListener listener;
        public Window CurrentWindow { get; private set; }

        public GameState(GameStateListener listener, Window initialWindow)
        {
            this.listener = listener;
            CurrentWindow = initialWindow;
        }

        public void ChangeWindow(Window newWindow)
        {
            CurrentWindow = newWindow;
            listener.OnStateChanged();
        }
    }
}
