using B20.Frontend.Elements;
using B20.Logic.Utils;
using Xunit;

namespace B20.Tests.Frontend.Elements.Tests
{
    public class ElementListVMTest
    {
        class SomeModel {}
        class SomeListVM: ElementListVM<SomeModel>
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
            var list = new SomeListVM();
            
            list.Update(ListUtils.Of(new SomeModel()));
            
            Assert.Equal(1, list.UpdateCount);
            
            list.Update(ListUtils.Of(new SomeModel(), new SomeModel(), new SomeModel()));
            
            Assert.Equal(2, list.UpdateCount);
            Assert.Equal(3, list.Model.Count);
        }
    }
}