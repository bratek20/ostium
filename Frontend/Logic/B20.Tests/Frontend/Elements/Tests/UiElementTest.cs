using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Exceptions.Fixtures;
using B20.Frontend.Traits.Context;
using B20.Frontend.UiElements.Context;
using B20.Frontend.UiElements.Utils;
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
        
    class UiElementTester: UiElement<SomeModel>
    {
        private int startCount = 0;
        private int updateCount = 0;

        public Label Label1 { get; set;  }
        public Button Button { get; set;  }
        public Label Label2 { get; set;  }

        protected override List<Type> GetTraitTypes()
        {
            return ListUtils.Of(
                typeof(TraitTester)
            );
        }

        protected override void OnStart()
        {
            startCount++;
        }

        protected override void OnUpdate()
        {
            updateCount++;
        }
        
        public void AssertStartCount(int expected)
        {
            Assert.Equal(expected, startCount);
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
        
        public void AssertHaveChildrenInOrderOfDeclaration()
        {
            AssertExt.ListCount(Children, 3);
            AssertExt.Equal(Children[0], Label1);
            AssertExt.Equal(Children[1], Button);
            AssertExt.Equal(Children[2], Label2);
        }
    }
    
    public class UiElementTest
    {
        private UiElementTester elementTester;
        private UiElement elementInterf;
        
        public UiElementTest()
        {
            elementTester = ContextsFactory.CreateBuilder()
                .WithModules(
                    new TraitsImpl(),
                    new UiElementsImpl()
                )
                .SetClass<TraitTester>()
                .SetClass<OtherTrait>()
                .SetClass<UiElementTester>()
                .Get<UiElementTester>();
            elementInterf = elementTester;
        }
            
        [Fact]
        public void ShouldCallOnUpdateEveryUpdateAndOnStartBeforeFirstOnUpdate()
        {
            elementTester.Update(new SomeModel());
            
            elementTester.AssertStartCount(1);
            elementTester.AssertUpdateCount(1);
            
            elementTester.Update(new SomeModel());
            
            elementTester.AssertStartCount(1);
            elementTester.AssertUpdateCount(2);
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
                    e.Message = "Trait OtherTrait not found for UiElementTester";
                }
            );
        }
        
        [Fact]
        public void ShouldSupportChildren()
        {
            elementTester.AssertHaveChildrenInOrderOfDeclaration();
        }
    }
}