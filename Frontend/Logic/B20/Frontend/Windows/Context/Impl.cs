using B20.Architecture.Contexts.Api;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;

namespace B20.Frontend.Windows.Context
{
    public class WindowsImpl: ContextModule
    {
        public void Apply(ContextBuilder builder)
        {
            builder.SetImpl<WindowManager, WindowManagerLogic>();
        }
    }
}