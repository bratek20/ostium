using Autofac;
using Autofac.Extensions.DependencyInjection;
using B20.Architecture.ContextModule.Api;

namespace B20.Architecture.ContextModule.Impl
{
    public class ContextLogic : Api.Context
    {
        private readonly ILifetimeScope _scope;

        public ContextLogic(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public T Get<T>() where T : class
        {
            return _scope.Resolve<T>();
        }
    }

    public class ContextBuilderLogic : ContextBuilder
    {
        private readonly ContainerBuilder _builder;

        public ContextBuilderLogic()
        {
            _builder = new ContainerBuilder();
        }

        public ContextBuilder SetClass<T>() where T : class
        {
            _builder.RegisterType<T>().AsSelf().SingleInstance().PropertiesAutowired();
            return this;
        }

        public ContextBuilder SetImpl<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            _builder.RegisterType<TImplementation>().As<TInterface>().PropertiesAutowired();
            return this;
        }

        public ContextBuilder SetImplObject<I>(I implementationObj) where I : class
        {
            _builder.RegisterInstance(implementationObj).PropertiesAutowired();
            return this;
        }

        public ContextBuilder WithModule(Api.ContextModule module)
        {
            module.Apply(this);
            return this;
        }

        public Api.Context Build()
        {
            var container = _builder.Build();
            return new ContextLogic(container);
        }
    }
}