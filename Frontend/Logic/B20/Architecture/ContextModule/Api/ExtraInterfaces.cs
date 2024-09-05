using System.Collections.Generic;

namespace B20.Architecture.ContextModule.Api
{
    public interface Context
    {
        T Get<T>() where T : class;
        //
        // ISet<T> GetMany<T>() where T : class;
    }

    public interface ContextBuilder
    {
        ContextBuilder SetClass<T>() where T : class;
        ContextBuilder SetImpl<TInterface, TImplementation>()             
            where TInterface : class
            where TImplementation : class, TInterface;
        
        // ContextBuilder AddClass<T>();
        //
        
        // ContextBuilder AddImpl<I, T>() where T : class, I;
        //
        // ContextBuilder SetImplObject<I>(I implementationObj) where I : class;
        // ContextBuilder AddImplObject<I>(I implementationObj) where I : class;

        ContextBuilder WithModule(ContextModule module);

        ContextBuilder WithModules(params ContextModule[] modules)
        {
            foreach (var module in modules)
            {
                WithModule(module);
            }
            return this;
        }

        Context Build();

        T Get<T>() where T : class
        {
            return Build().Get<T>();
        }
    }

    public interface ContextModule
    {
        void Apply(ContextBuilder builder);
    }
}