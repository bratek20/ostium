using System.Collections.Generic;
using B20.Events.Api;
using B20.Events.Impl;
using B20.Ext;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Tests.Frontend.Windows.Fixtures;
using GameComponents.Api;

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
        
        public class InGameWindowContext : Context
        {
            public InGameWindowContext(EventPublisher eventPublisher, WindowManager windowManager, OstiumLogic logic) : base(eventPublisher, windowManager, logic)
            {
            }
            
            public GameWindow GameWindow => WindowManager.Get(WindowIds.GAME_WINDOW) as GameWindow;
            
            public RowVM AttackRow => GameWindow.Game.Table.AttackRow;
            public RowVM DefenseRow => GameWindow.Game.Table.DefenseRow;
            
            public List<CreatureCardVm> CardsInHand => GameWindow.Game.Hand.Cards.Model;
            public CreatureCardVm FirstCardInHand => CardsInHand[0];
            
            public Optional<CreatureCardId> SelectedCard => GameWindow.Game.SelectedCard;
        }
        public InGameWindowContext InGameWindow()
        {
            var c = Setup();
            c.Logic.RegisterWindows();
            c.Logic.Start();
            (c.WindowManager.Get(WindowIds.MAIN_WINDOW) as MainWindow).PlayButton.Click();
            
            return new InGameWindowContext(
                eventPublisher: c.EventPublisher,
                windowManager: c.WindowManager,
                logic: c.Logic
            );
        }
    }
}