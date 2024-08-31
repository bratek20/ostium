using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Tests.Architecture.Events.Fixtures;
using B20.Tests.Frontend.Types.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class DraggableTest
    {
        class SomeModel {}
        class SomeElementVM : ElementVm<SomeModel>
        {
        }

        private EventPublisherMock publisherMock;
        private Position2dVm positionVm;
        private Draggable draggable;
        private SomeElementVM owner;
        
        public DraggableTest()
        {
            publisherMock = new EventPublisherMock();
            positionVm = new Position2dVm();
            draggable = new Draggable(publisherMock, positionVm);
            owner = new SomeElementVM();
            draggable.Init(owner);
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