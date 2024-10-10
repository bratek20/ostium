using System;
using B20.Frontend.UiElements;

namespace MyNamespace
{
    public class MyModel
    {
    }

    public class MyViewModel : UiElement<MyModel>
    {

    }
    
    public class MyViewModelGroup : UiElementGroup<MyViewModel, MyModel>
    {
        public MyViewModelGroup(Func<MyViewModel> elementFactory) : base(elementFactory)
        {
        }
    }
}
