using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Core.Registration;
using Autofac.Extensions.DependencyInjection;
using B20.Architecture.Contexts.Api;

namespace B20.Architecture.Contexts.Impl
{
    public class ContextLogic : Api.Context
    {
        private readonly ILifetimeScope _scope;

        public ContextLogic(ContainerBuilder builder)
        {
            builder.RegisterInstance(this).As<Api.Context>();
            _scope = builder.Build();
        }

        public T Get<T>() where T : class
        {
            return Resolve<T>();
        }

        public IEnumerable<T> GetMany<T>() where T : class
        {
            return Resolve<IEnumerable<T>>();
        }
        
        private T Resolve<T>()
        {
            try
            {
                return _scope.Resolve<T>();    
            }
            catch (DependencyResolutionException e)
            {
                if (e.InnerException is DependentClassNotFoundInContextException)
                {
                    throw e.InnerException;
                }
                if (e is ComponentNotRegisteredException)
                {
                    throw new ClassNotFoundInContextException($"Class '{typeof(T).Name}' not found in context");
                }

                throw;
            }
            
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
            return Register(b => b.RegisterType<TImplementation>().AsSelf().As<TInterface>());
        }
        
        public ContextBuilder AddImpl<TInterface, TImplementation>()
            where TInterface : class
            where TImplementation : class, TInterface
        {
            return Register(b => b.RegisterType<TImplementation>().AsSelf().As<TInterface>());
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

            // Add validation logic to ensure fields are injected
            registration.OnActivated(e =>
            {
                var properties = typeof(T).GetProperties()
                    .Where(p => p.CanWrite && p.GetSetMethod(true).IsPublic);

                foreach (var prop in properties)
                {
                    var value = prop.GetValue(e.Instance);
                    if (value == null)
                    {
                        var message = $"Type '{prop.PropertyType.Name}' could not be injected to '{typeof(T).Name}'";
                        throw new DependentClassNotFoundInContextException(message);
                    }
                }
            });
            
            if (mode == InjectionMode.Singleton)
            {
                registration.SingleInstance();    
            }
            return this;
        }

        public Api.Context Build()
        {
            return new ContextLogic(_builder);
        }
    }
}