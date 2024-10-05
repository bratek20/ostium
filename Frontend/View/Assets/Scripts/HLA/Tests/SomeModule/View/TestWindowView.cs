using B20.Frontend.Elements.View;
using UnityEngine;

namespace SomeNamespace
{
    public class TestWindowView : WindowView<EmptyWindow>
    {
        [SerializeField]
        LabelView myLabel;
    }
}

