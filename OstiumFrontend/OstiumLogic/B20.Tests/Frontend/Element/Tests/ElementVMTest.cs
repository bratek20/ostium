using B20.Frontend.Element;
using Xunit;

namespace B20.Frontend.Element.Tests
{
    public class ElementVMTest
    {
        class SomeModel {}
        
        class SomeElementVM: ElementVM<SomeModel>
        {
            public int UpdateCount { get; private set; }
            
            protected override void OnUpdate()
            {
                UpdateCount++;
            }
        }
        
        [Fact]
        public void ShouldCallOnUpdate()
        {
            var element = new SomeElementVM();
            
            element.Update(new SomeModel());
            
            Assert.Equal(1, element.UpdateCount);
        }
    }
}