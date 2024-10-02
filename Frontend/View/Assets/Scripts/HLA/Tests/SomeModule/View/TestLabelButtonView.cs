using System.Collections;
using System.Collections.Generic;
using B20.Frontend.Elements.View;
using B20.Frontend.UiElements;
using SomeNamespace;
using UnityEngine;

namespace SomeNamespace
{
    public class TestLabelButtonView : ElementView<EmptyViewModel>
    {
        [SerializeField]
        LabelView myLabel;
        [SerializeField]
        ButtonView myButton;
        
        public LabelView MyLabel => myLabel;
        public ButtonView MyButton => myButton;
    }
}

