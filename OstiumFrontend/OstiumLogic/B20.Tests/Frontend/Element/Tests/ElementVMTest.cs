using B20.Tests.Asserts;
using Xunit;

namespace B20.Frontend.Element.Tests
{
    class TraitTester : Trait
    {
        public void AssertOwner(ElementVM owner)
        {
            Assert.Equal(owner, Owner);
        }
    }
    
    class SomeModel {}
        
    class ElementVMTester: ElementVM<SomeModel>
    {
        private int updateCount = 0;
        
        public ElementVMTester()
        {
            AddTrait(new TraitTester());
        }
        
        protected override void OnUpdate()
        {
            updateCount++;
        }
        
        public void AssertUpdateCount(int expected)
        {
            Assert.Equal(expected, updateCount);
        }
        
        public void AssertHaveTraitTesterWithOwnerInitialized()
        {
            var t = GetTrait<TraitTester>();
            t.AssertOwner(this);
        }
    }
    
    public class ElementVMTest
    {
        private ElementVMTester elementTester;
        private ElementVM elementInterf;
        
        public ElementVMTest()
        {
            elementTester = new ElementVMTester();
            elementInterf = elementTester;
        }
            
        [Fact]
        public void ShouldCallOnUpdate()
        {
            elementTester.Update(new SomeModel());
            
            elementTester.AssertUpdateCount(1);
        }
        
        [Fact]
        public void ShouldSupportTraits()
        {
            elementTester.AssertHaveTraitTesterWithOwnerInitialized();
            
            Asserts.ListCount(elementInterf.GetTraits(), 1);
        }
    }
}