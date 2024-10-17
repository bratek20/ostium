using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Context;
using B20.Frontend.Traits;
using B20.Frontend.UiElements;
using B20.Frontend.Windows.Api;
using B20.Tests.Frontend.TestHelpers;
using B20.Tests.Frontend.Traits.Fixtures;
using B20.Tests.Frontend.Types.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public abstract class DraggableElementBase : UiElement<EmptyModel>
    {
        protected override List<Type> GetTraitTypes()
        {
            return new List<Type> { typeof(WithPosition2d), typeof(Draggable) };
        }
    }
    
    public class DraggableElement1: DraggableElementBase { }
    public class DraggableElement2: DraggableElementBase { }
    
    public class ElementWithRect : UiElement<EmptyModel>
    {
        protected override List<Type> GetTraitTypes()
        {
            return new List<Type> { typeof(WithRect) };
        }
    }
    
    public class TestWindow : Window<EmptyWindowState>
    {
        public DraggableElement1 DraggableElement1 { get; set; }
        public ElementWithRect ElementWithRect { get; set; }
        public DraggableElement2 DraggableElement2 { get; set; }
    }
    
    public class TraitsHelperTest
    {
        [Fact]
        public void ShouldPlaceElementsOnXAxis()
        {
            var window = ContextsFactory.CreateBuilder()
                .WithModule(new ViewModelTesting())
                .SetClass<DraggableElement1>()
                .SetClass<DraggableElement2>()
                .SetClass<ElementWithRect>()
                .SetClass<TestWindow>()
                .Get<TestWindow>();
            
            TraitsHelpers.PlaceElements(window);
            
            AssertTypes.Position2d(
                window.DraggableElement1.GetTrait<WithPosition2d>().Model,
                1, 0
            );
            
            AssertTypes.Rect(
                window.ElementWithRect.GetTrait<WithRect>().Rect,
                2, 1, 1, 1
            );
            
            AssertTypes.Position2d(
                window.DraggableElement2.GetTrait<WithPosition2d>().Model,
                4, 0
            );
        }
    }
}