using System;
using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using B20.Architecture.Contexts.Api;

namespace B20.Architecture.Contexts.Impl
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

        public ContextBuilder SetClass<T>(InjectionMode mode = InjectionMode.Singleton) where T : class
        {
            return Register(b => b.RegisterType<T>().AsSelf(), mode);
        }

        public ContextBuilder SetImpl<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return Register(b => b.RegisterType<TImplementation>().As<TInterface>());
        }

        public ContextBuilder SetImplObject<I>(I implementationObj) where I : class
        {
            return Register(b => b.RegisterInstance(implementationObj));
        }

        public ContextBuilder WithModule(Api.ContextModule module)
        {
            module.Apply(this);
            return this;
        }

        private ContextBuilder Register<T>(
            Func<ContainerBuilder, IRegistrationBuilder<T, object, object>> registrationAction,
            InjectionMode mode = InjectionMode.Singleton
        )
            where T : class
        {
            var registration = registrationAction(_builder);
            registration.PropertiesAutowired();

            if (mode == InjectionMode.Singleton)
            {
                registration.SingleInstance();    
            }
            return this;
        }

        public Api.Context Build()
        {
            var container = _builder.Build();
            return new ContextLogic(container);
        }
    }
}