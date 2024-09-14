using System;
using System.Collections.Generic;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Architecture.Events.Fixtures;
using B20.Logic.Utils;
using B20.Tests.Frontend.Traits.Fixtures;
using B20.Tests.Frontend.Types.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class DraggableTest
    {
        class SomeModel {}
        class SomeElementVM : ElementVm<SomeModel>
        {
            protected override List<Type> GetTraitTypes()
            {
                return ListUtils.Of(
                    typeof(WithPosition2d),
                    typeof(Draggable)
                );
            }
        }

        private EventPublisherMock publisherMock;
        private WithPosition2d positionVm;
        private Draggable draggable;
        private SomeElementVM owner;
        
        public DraggableTest()
        {
            var r = TraitsScenarios.Setup(a =>
            {
                a.ContextManipulation = b =>
                {
                    b.SetClass<SomeElementVM>();
                };
            });
            publisherMock = r.PublisherMock;
            owner = r.Context.Get<SomeElementVM>();
            draggable = owner.GetTrait<Draggable>();
            positionVm = owner.GetTrait<WithPosition2d>();
        }

        [Fact]
        public void StartDrag_PublishDragStartedAndPositionVmSet()
        {
            var position = Builders.CreatePosition2D();
            
            draggable.StartDrag(position);

            publisherMock.AssertOneEventPublished<ElementDragStartedEvent>(
                e => e.Element.Equals(owner) && e.Position.Equals(position)
            );
            Assert.Equal(positionVm.Model, position);
        }
        
        [Fact]
        public void OnDrag_PositionVmUpdated()
        {
            draggable.StartDrag(Builders.CreatePosition2D());
            
            draggable.OnDrag(new Position2d(1, 2));

            Assert.Equal(positionVm.Model, new Position2d(1, 2));
        }

        [Fact]
        public void EndDrag_PublishDragEndedAndReturnToStartPosition()
        {
            var startPosition = new Position2d(1, 1);
            var endPosition = new Position2d(2, 2);
            
            draggable.StartDrag(startPosition);
            publisherMock.Reset();
            
            draggable.EndDrag(endPosition);

            publisherMock.AssertOneEventPublished<ElementDragEndedEvent>(
                e => e.Element.Equals(owner) && e.Position.Equals(endPosition)
            );
            Assert.Equal(positionVm.Model, startPosition);
        }
    }
}