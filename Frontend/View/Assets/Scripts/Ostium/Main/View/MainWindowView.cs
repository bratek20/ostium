using B20.View;
using Main.ViewModel;
using UnityEngine;

namespace Main.View
{
    public class MainWindowView: WindowView<MainWindow>
    {
        [SerializeField] 
        private ButtonView playButton;

        protected override void OnInit()
        {
            playButton.Init(ViewModel.PlayButton);
        }
    }
}