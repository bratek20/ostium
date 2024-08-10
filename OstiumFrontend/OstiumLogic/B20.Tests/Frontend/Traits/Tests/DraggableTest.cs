using B20.Frontend.Element;
using B20.Frontend.Traits;
using B20.Tests.Architecture.Events.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class DraggableTest
    {
        class SomeModel {}
        class SomeElementVM : ElementVM<SomeModel>
        {
        }
        
        class SimplePosition : Position
        {
        }

        [Fact]
        public void StartDrag_PublishDragStarted()
        {
            var publisherMock = new EventPublisherMock();
            var draggable = new Draggable(publisherMock);
            var owner = new SomeElementVM();
            draggable.Init(owner);

            var position = new SimplePosition();
            
            draggable.StartDrag(position);

            publisherMock.AssertOneEventPublished<ElementDragStartedEvent>(
                e => e.Element.Equals(owner) && e.Position.Equals(position)
            );
        }

        [Fact]
        public void EndDrag_PublishDragEnded()
        {
            var publisherMock = new EventPublisherMock();
            var draggable = new Draggable(publisherMock);
            var owner = new SomeElementVM();
            draggable.Init(owner);
        
            var position = new SimplePosition();
            
            draggable.EndDrag(position);

            publisherMock.AssertOneEventPublished<ElementDragEndedEvent>(
                e => e.Element.Equals(owner) && e.Position.Equals(position)
            );
        }
    }
}