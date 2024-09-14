using System;
using B20.Frontend.UiElements;
using B20.Logic.Utils;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class SomeModel
    {
        public int Value { get; }

        public SomeModel(int value)
        {
            Value = value;
        }
    }
    public class SomeViewModel: UiElement<SomeModel> {}
    
    public class ElementListVMTest
    {
        [Fact]
        public void ShouldCreateVmElementsWithModel()
        {
            var list = new UiElementGroup<SomeViewModel, SomeModel>(() => new SomeViewModel());
            
            list.Update(ListUtils.Of(new SomeModel(1)));
            AssertExt.ListCount(list.Elements, 1);
            Assert.Equal(1, list.Elements[0].Model.Value);
            
            list.Update(ListUtils.Of(new SomeModel(1), new SomeModel(2)));
            AssertExt.ListCount(list.Elements, 2);
            Assert.Equal(1, list.Elements[0].Model.Value);
            Assert.Equal(2, list.Elements[1].Model.Value);
        }
    }
}