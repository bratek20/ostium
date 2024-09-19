using System;

namespace B20.Frontend.Timer.Api
{
    public interface TimerApi
    {
        void Progress(int ms);
        
        void Schedule(Action action, int everySeconds);
    }
}