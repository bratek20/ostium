using B20.Architecture.ContextModule.Api;
using B20.Architecture.ContextModule.Impl;

namespace B20.Architecture.ContextModule.Context
{
    public class ContextModuleFactory
    {
        public static ContextBuilder CreateBuilder()
        {
            return new ContextBuilderLogic();
        }
    }
}