using System.Collections;
using System.Collections.Generic;
using B20.Frontend.Elements.View;
using UnityEngine;

namespace SomeNamespace
{
    public class TestElementGroupView : UiElementGroupView<TestLabelButtonView, EmptyViewModel, EmptyModel>
    {
        public TestLabelButtonView ElementPrefab => elementPrefab;
    }
}

