using B20.Frontend.Elements.View;

namespace SomeNamespace
{
    public class TestElementGroupView : UiElementGroupView<TestLabelButtonView, EmptyViewModel, EmptyModel>
    {
        public TestLabelButtonView ElementPrefab => elementPrefab;
    }
}

