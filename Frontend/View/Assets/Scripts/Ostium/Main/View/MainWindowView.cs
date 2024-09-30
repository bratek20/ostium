using B20.Frontend.Elements.View;
using Main.ViewModel;
using UnityEngine;

namespace Main.View
{
    public class MainWindowView: WindowView<MainWindow>
    {
        [SerializeField] 
        private InputFieldView username;
        [SerializeField] 
        private ButtonView play;

        protected override void OnBind()
        {
            username.Bind(ViewModel.Username);
            play.Bind(ViewModel.PlayButton);
        }
    }
}