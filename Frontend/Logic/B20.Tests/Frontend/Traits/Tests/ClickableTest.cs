using B20.Architecture.Contexts.Context;
using B20.Frontend.UiElements;
using B20.Frontend.Traits;
using B20.Architecture.Events.Fixtures;
using B20.Frontend.Traits.Context;
using B20.Tests.Architecture.Events.Context;
using Xunit;

namespace B20.Tests.Frontend.Traits.Tests
{
    public class ClickableTest
    {
        class SomeModel {}
        class SomeElementVM: UiElement<SomeModel>
        {
        }
        
        [Fact]
        public void Click_PublishClicked()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new EventsMocks(),
                    new TraitsImpl()
                )
                .Build();
            var publisherMock = c.Get<EventPublisherMock>();
            var clickable = c.Get<TraitFactory>().Create(typeof(Clickable)) as Clickable;

            var owner = new SomeElementVM();
            clickable.Init(owner);
            
            clickable.Click();
            
            publisherMock.AssertOneEventPublished<ElementClickedEvent>(
                e => e.Element.Equals(owner)
            );
        }
    }
}