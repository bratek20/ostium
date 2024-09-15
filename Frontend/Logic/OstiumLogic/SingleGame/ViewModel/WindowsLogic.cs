namespace SingleGame.ViewModel
{
    public partial class GameWindow
    {
        public void OnOpen()
        {
            Game.UpdateState();
        }
    }
}