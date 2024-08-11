using System;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Logic.Utils;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class ElementListVMTest
    {
        class SomeModel
        {
            public int Value { get; }

            public SomeModel(int value)
            {
                Value = value;
            }
        }
        class SomeViewModel: ElementVm<SomeModel> {}

        [Fact]
        public void ShouldCreateVmElementsWithModel()
        {
            var list = new ElementListVm<SomeViewModel, SomeModel>(() => new SomeViewModel());
            
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