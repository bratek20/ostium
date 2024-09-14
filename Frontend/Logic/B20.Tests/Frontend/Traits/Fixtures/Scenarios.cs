using System;
using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Events.Fixtures;
using B20.Frontend.UiElements;
using B20.Frontend.Traits.Context;
using B20.Tests.Architecture.Events.Context;

namespace B20.Tests.Frontend.Traits.Fixtures
{
    public class TraitsScenarios
    {
        public class SetupArgs
        {
            public Action<ContextBuilder> ContextManipulation { get; set; } = null;
        }
        
        public class SetupResult
        {
            public EventPublisherMock PublisherMock { get; set; }
            public TraitFactory TraitFactory { get; set; }
            public Context Context { get; set; }
        }

        public static SetupResult Setup(Action<SetupArgs> init = null)
        {
            var args = new SetupArgs();
            init?.Invoke(args);
            
            var builder = ContextsFactory.CreateBuilder()
                .WithModules(
                    new EventsMocks(),
                    new TraitsImpl()
                );
            
            args?.ContextManipulation?.Invoke(builder);

            var c = builder.Build();
            return new SetupResult
            {
                PublisherMock = c.Get<EventPublisherMock>(),
                TraitFactory = c.Get<TraitFactory>(),
                Context = c
            };
        }
    }
}