using System;
using System.Collections.Generic;
using B20.Events.Api;
using B20.Events.Impl;
using B20.Ext;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Impl;
using B20.Tests.Frontend.Windows.Fixtures;
using GameComponents.Api;
using GameSetup.Impl;

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
            var logic = OstiumLogicFactory.Create(new WindowManipulatorMock(), true);
            return new Context(
                eventPublisher: logic.EventPublisher,
                windowManager: logic.WindowManager,
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
            
            public CreatureCardVm CardInAttackRow => AttackRow.Card.Element;
            public CreatureCardVm CardInDefenseRow => DefenseRow.Card.Element;
            
            public List<CreatureCardVm> CardsInHand => GameWindow.Game.Hand.Cards.Elements;
            public CreatureCardVm FirstCardInHand => CardsInHand[0];
            
            public Optional<CreatureCardVm> SelectedCard => GameWindow.Game.SelectedCard;
        }

        public class InGameWindowArgs
        {
            public Rect AttackRowRect { get; set; } = new Rect(0, 0, 100, 100);
            public Rect DefenseRowRect { get; set; } = new Rect(0, 100, 100, 100);
        }
        public InGameWindowContext InGameWindow(Action<InGameWindowArgs> init = null)
        {
            var args = new InGameWindowArgs();
            init?.Invoke(args);
            
            var c = Setup();
            c.Logic.Start();
            (c.WindowManager.Get(WindowIds.MAIN_WINDOW) as MainWindow).PlayButton.Click();
            
            var nc = new InGameWindowContext(
                eventPublisher: c.EventPublisher,
                windowManager: c.WindowManager,
                logic: c.Logic
            );
            nc.AttackRow.GetTrait<WithRect>().RectProvider = () => args.AttackRowRect;
            nc.DefenseRow.GetTrait<WithRect>().RectProvider = () => args.DefenseRowRect;
            return nc;
        }
    }
}