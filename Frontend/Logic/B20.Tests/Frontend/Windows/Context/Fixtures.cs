using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using B20.Tests.Frontend.Windows.Fixtures;

namespace B20.Tests.Frontend.Windows.Context
{
    public class WindowsMocks: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder
                .SetImpl<WindowManager, WindowManagerMock>();
        }
    }
}