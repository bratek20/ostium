using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Impl;

namespace B20.Architecture.Contexts.Context
{
    public class ContextModuleFactory
    {
        public static ContextBuilder CreateBuilder()
        {
            return new ContextBuilderLogic();
        }
    }
}