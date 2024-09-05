using Microsoft.Extensions.DependencyInjection;
using System;
using B20.Architecture.ContextModule.Api;

namespace B20.Architecture.ContextModule.Impl
{
    public class ContextLogic : Context
    {
        private readonly IServiceProvider _provider;

        public ContextLogic(IServiceProvider provider)
        {
            _provider = provider;
        }

        public T Get<T>() where T : class
        {
            return _provider.GetService<T>();
        }
    }

    public class ContextBuilderLogic : ContextBuilder
    {
        private readonly IServiceCollection _services;

        public ContextBuilderLogic()
        {
            _services = new ServiceCollection();
        }

        public ContextBuilder SetClass<T>() where T : class
        {
            _services.AddTransient<T>();
            return this;
        }

        public ContextBuilder SetImpl<TInterface, TImplementation>()             
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _services.AddTransient<TInterface, TImplementation>();
            return this;
        }

        public ContextBuilder SetImplObject<I>(I implementationObj) where I : class
        {
            _services.AddSingleton(implementationObj);
            return this;
        }

        public ContextBuilder WithModule(Api.ContextModule module)
        {
            module.Apply(this);
            return this;
        }

        public Context Build()
        {
            var provider = _services.BuildServiceProvider();
            return new ContextLogic(provider);
        }
    }
}