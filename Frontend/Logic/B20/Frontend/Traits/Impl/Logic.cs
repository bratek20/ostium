using System;
using B20.Frontend.UiElements;

namespace B20.Frontend.Traits.Impl
{
    public class TraitFactoryLogic: TraitFactory
    {
        private Architecture.Contexts.Api.Context _context;

        public TraitFactoryLogic(Architecture.Contexts.Api.Context context)
        {
            _context = context;
        }

        public Trait Create(Type type)
        {
            // Get the MethodInfo for the generic method 'Get<T>()'
            var method = _context.GetType().GetMethod("Get").MakeGenericMethod(type);

            // Invoke the method on the _context instance
            return (Trait)method.Invoke(_context, null);
        }

        public T Create<T>() where T : Trait
        {
            return (T)Create(typeof(T));
        }
    }
}