using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Frontend.Element.Tests
{
    class TraitTester : Trait
    {
        public void AssertOwner(ElementVm owner)
        {
            Assert.Equal(owner, Owner);
        }
    }
    
    class OtherTrait : Trait {}
    
    class SomeModel {}
        
    class ElementVMTester: ElementVm<SomeModel>
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
        private ElementVm elementInterf;
        
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
            
            B20.Tests.Architecture.Exceptions.Fixtures.Asserts.ThrowsApiException(
                () => elementInterf.GetTrait<OtherTrait>(),
                e =>
                {
                    e.Type = typeof(TraitNotFoundException);
                    e.Message = "Trait OtherTrait not found for ElementVMTester";
                }
            );
        }
    }
}