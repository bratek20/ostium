using B20.Architecture.Contexts.Api;
using B20.Architecture.Contexts.Impl;

namespace B20.Architecture.Contexts.Context
{
    public class ContextsFactory
    {
        public static ContextBuilder CreateBuilder()
        {
            return new ContextBuilderLogic();
        }
    }
}