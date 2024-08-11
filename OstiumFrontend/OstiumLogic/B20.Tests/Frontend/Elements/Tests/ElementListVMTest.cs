using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Logic.Utils;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class ElementListVMTest
    {
        class SomeModel {}
        class SomeViewModel: ElementVm<SomeModel> {}
        class SomeListVm: ElementListVm<SomeViewModel>
        {
            public int UpdateCount { get; private set; }
            
            protected override void OnUpdate()
            {
                UpdateCount++;
            }
        }
        
        [Fact]
        public void ShouldWork()
        {
            var list = new SomeListVm();
            
            list.Update(ListUtils.Of(new SomeViewModel()));
            
            Assert.Equal(1, list.UpdateCount);
            
            list.Update(ListUtils.Of(new SomeViewModel(), new SomeViewModel(), new SomeViewModel()));
            
            Assert.Equal(2, list.UpdateCount);
            Assert.Equal(3, list.Model.Count);
        }
    }
}