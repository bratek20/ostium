using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Exceptions.Fixtures;
using B20.Frontend.Traits.Context;
using B20.Logic.Utils;
using B20.Tests.ExtraAsserts;
using Xunit;

namespace B20.Frontend.UiElements.Tests
{
    class TraitTester : Trait
    {
        public void AssertOwner(UiElement owner)
        {
            Assert.Equal(owner, Owner);
        }
    }
    
    class OtherTrait : Trait {}
    
    class SomeModel {}
        
    class ElementVMTester: UiElement<SomeModel>
    {
        private int updateCount = 0;

        protected override List<Type> GetTraitTypes()
        {
            return ListUtils.Of(
                typeof(TraitTester)
            );
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
        private UiElement elementInterf;
        
        public ElementVMTest()
        {
            elementTester = ContextsFactory.CreateBuilder()
                .WithModule(new TraitsImpl())
                .SetClass<TraitTester>()
                .SetClass<OtherTrait>()
                .SetClass<ElementVMTester>()
                .Get<ElementVMTester>();
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
            AssertExt.ListCount(elementInterf.Traits, 1);
            
            elementTester.AssertHaveTraitTesterWithOwnerInitialized();

            ExceptionsAsserts.ThrowsApiException(
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