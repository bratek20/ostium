using B20.Frontend.Element;
using B20.Frontend.Traits;
using B20.Architecture.Events.Fixtures;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class ClickableTest
    {
        class SomeModel {}
        class SomeElementVM: ElementVm<SomeModel>
        {
        }
        
        [Fact]
        public void Click_PublishClicked()
        {
            var publisherMock = new EventPublisherMock();
            var clickable = new Clickable(publisherMock);
            var owner = new SomeElementVM();
            clickable.Init(owner);
            
            clickable.Click();
            
            publisherMock.AssertOneEventPublished<ElementClickedEvent>(
                e => e.Element.Equals(owner)
            );
        }
    }
}