using B20.Frontend.Elements.View;
using UnityEngine;

namespace SomeNamespace
{
    public class TestReferencingView2: ElementView<EmptyViewModel>
    {
        [SerializeField]
        TestLabelButtonView myLabelButton;
        
        public TestLabelButtonView MyLabelButton => myLabelButton;
    }
}