using System;
using System.Collections.Generic;
using B20.Frontend.Timer.Api;

namespace B20.Frontend.Timer.Impl
{
    class ScheduledAction
    {
        public Action Action;
        public int EverySeconds;
        public int RemainingTimeMs;
    }
    
    public class TimerApiLogic: TimerApi
    {
        private List<ScheduledAction> _scheduledActions = new List<ScheduledAction>();
        
        public void Progress(int ms)
        {
            foreach (var scheduledAction in _scheduledActions)
            {
                scheduledAction.RemainingTimeMs -= ms;
                if (scheduledAction.RemainingTimeMs <= 0)
                {
                    scheduledAction.Action();
                    scheduledAction.RemainingTimeMs = scheduledAction.EverySeconds * 1000;
                }
            }
        }

        public void Schedule(Action action, int everySeconds)
        {
            _scheduledActions.Add(new ScheduledAction
            {
                Action = action,
                EverySeconds = everySeconds,
                RemainingTimeMs = everySeconds * 1000
            });
        }
    }
}