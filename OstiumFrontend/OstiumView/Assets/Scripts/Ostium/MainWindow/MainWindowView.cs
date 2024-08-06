using System;
using B20.View;
using UnityEngine;

namespace Ostium.View.MainWindow
{
    public class MainWindowView: WindowView
    {
        [SerializeField] 
        private ButtonView playButton;

        protected override void OnInit()
        {
            playButton.Init(ValueAs<Logic.MainWindow>().PlayButton);
        }
    }
}