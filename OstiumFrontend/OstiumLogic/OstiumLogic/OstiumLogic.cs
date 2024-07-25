using B20.Logic;

namespace Ostium.Logic
{
    public class OstiumLogic
    {
        public GameState State { get; private set; }

        public OstiumLogic(GameStateListener listener)
        {
            State = new GameState(listener, new MainWindow());
        }
    }
}