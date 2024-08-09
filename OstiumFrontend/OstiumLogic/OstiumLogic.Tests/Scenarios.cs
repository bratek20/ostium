using B20.Events.Api;
using B20.Events.Impl;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Tests.Frontend.Windows.Fixtures;

namespace Ostium.Logic.Tests
{
    public class Scenarios
    {
        public class Context
        {
            public EventPublisher EventPublisher { get; }
            public WindowManager WindowManager { get; }
            public OstiumLogic Logic { get; }

            public Context(EventPublisher eventPublisher, WindowManager windowManager, OstiumLogic logic)
            {
                this.EventPublisher = eventPublisher;
                this.WindowManager = windowManager;
                this.Logic = logic;
            }
        }
        
        public Context Setup()
        {
            var eventPublisher = new EventPublisherLogic();
            var windowManager = new WindowManagerLogic(new WindowManipulatorMock());
            var logic = new OstiumLogic(eventPublisher, windowManager);
            
            return new Context(
                eventPublisher: eventPublisher,
                windowManager: windowManager,
                logic: logic
            );
        }

        public Context InGameWindow()
        {
            var c = Setup();
            c.Logic.RegisterWindows();
            c.Logic.Start();
            (c.WindowManager.Get(WindowIds.MAIN_WINDOW) as MainWindow).PlayButton.Click();
            return c;
        }
    }
}