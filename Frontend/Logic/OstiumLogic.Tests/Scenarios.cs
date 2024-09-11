using System;
using System.Collections.Generic;
using B20.Architecture.Contexts.Context;
using B20.Architecture.Events.Context;
using B20.Events.Api;
using B20.Events.Impl;
using B20.Ext;
using B20.Frontend.Postion;
using B20.Frontend.Traits;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Context;
using B20.Frontend.Windows.Impl;
using B20.Tests.Frontend.Windows.Fixtures;
using GameModule;
using GameModule.ViewModel;
using Main.ViewModel;
using Ostium.Logic.Tests.GameModule.Context;

namespace Ostium.Logic.Tests
{
    public class Scenarios
    {
        public class Context
        {
            public EventPublisher EventPublisher { get; }
            public WindowManager WindowManager { get; }
            public OstiumLogic Logic { get; }
            public GameApiMock GameApiMock { get; }

            public Context(
                EventPublisher eventPublisher, 
                WindowManager windowManager, 
                OstiumLogic logic,
                GameApiMock gameApiMock
            )
            {
                this.EventPublisher = eventPublisher;
                this.WindowManager = windowManager;
                this.Logic = logic;
                this.GameApiMock = gameApiMock;
            }
        }
        
        public Context Setup()
        {
            var c = ContextsFactory.CreateBuilder()
                .WithModules(
                    new GameModuleMocks(),
                    new WindowManipulatorInMemoryImpl(),
                    new WindowsImpl(),
                    new EventsImpl(),
                    new OstiumLogicPartialImpl()
                )
                .Build();
            
            return new Context(
                eventPublisher: c.Get<EventPublisher>(),
                windowManager: c.Get<WindowManager>(),
                logic: c.Get<OstiumLogic>(),
                gameApiMock: c.Get<GameApiMock>()
            );
        }
        
        public class InGameWindowContext : Context
        {
            public InGameWindowContext(EventPublisher eventPublisher, WindowManager windowManager, OstiumLogic logic, GameApiMock gameApiMock)
                : base(eventPublisher, windowManager, logic, gameApiMock)
            {
            }
            
            public GameWindow GameWindow => WindowManager.Get<GameWindow>();
            
            public RowVm AttackRow => GameWindow.Game.Table.AttackRow;
            public RowVm DefenseRow => GameWindow.Game.Table.DefenseRow;
            
            public CreatureCardVm CardInAttackRow => AttackRow.Card.Element;
            public CreatureCardVm CardInDefenseRow => DefenseRow.Card.Element;
            
            public List<CreatureCardVm> CardsInHand => GameWindow.Game.Hand.Cards.Elements;
            public CreatureCardVm FirstCardInHand => CardsInHand[0];
            public CreatureCardVm SecondCardInHand => CardsInHand[1];
            
            public Optional<CreatureCardVm> SelectedCard => GameWindow.Game.SelectedCard;
        }

        public class InGameWindowArgs
        {
            public Action<Builders.GameDef> Game = null;
            public Rect AttackRowRect { get; set; } = new Rect(0, 0, 100, 100);
            public Rect DefenseRowRect { get; set; } = new Rect(0, 100, 100, 100);
        }
        public InGameWindowContext InGameWindow(Action<InGameWindowArgs> init = null)
        {
            var args = new InGameWindowArgs();
            init?.Invoke(args);
            
            var c = Setup();
            c.GameApiMock.SetGame(args.Game);
            c.Logic.Start();
            c.WindowManager.Get<MainWindow>().PlayButton.Click();
            
            var nc = new InGameWindowContext(
                eventPublisher: c.EventPublisher,
                windowManager: c.WindowManager,
                logic: c.Logic,
                gameApiMock: c.GameApiMock
            );
            nc.AttackRow.GetTrait<WithRect>().RectProvider = () => args.AttackRowRect;
            nc.DefenseRow.GetTrait<WithRect>().RectProvider = () => args.DefenseRowRect;
            return nc;
        }
    }
}