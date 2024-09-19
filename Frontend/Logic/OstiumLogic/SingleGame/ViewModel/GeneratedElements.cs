using System;
using System.Collections.Generic;
using B20.Frontend.UiElements;
using B20.Frontend.Traits;
using SingleGame.Api;

namespace SingleGame.ViewModel
{
    public partial class CreatureCardVm: UiElement<CreatureCard>
    {
        public Label Name { get; } = new Label();
        public BoolSwitch Selected { get; } = new BoolSwitch();
        
        protected override List<Type> GetTraitTypes()
        {
            return new List<Type>() {typeof(WithPosition2d), typeof(Draggable)};
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
    }
    
    public class CreateCardListVm: UiElementGroup<CreatureCardVm, CreatureCard>
    {
        public CreateCardListVm(B20.Architecture.Contexts.Api.Context c) : base(() => c.Get<CreatureCardVm>())
        {
            
        }
    }
    
    public partial class HandVm: UiElement<Hand>
    {
        public CreateCardListVm Cards { get; set; }

        protected override void OnUpdate()
        {
            Cards.Update(Model.GetCards());
        }
    }
    
    public class OptionalCreatureCardVm: OptionalUiElement<CreatureCardVm, CreatureCard>
    {
        public OptionalCreatureCardVm(CreatureCardVm element) : base(element)
        {
        }
    }
    
    public partial class RowVm: UiElement<Row>
    {
        public OptionalCreatureCardVm Card { get; set; }
        
        protected override List<Type> GetTraitTypes()
        {
            return new List<Type>() {typeof(WithRect)};
        }
        
        protected override void OnUpdate()
        {
            Card.Update(Model.GetCard());
        }
    }
    
    public partial class PlayerSideVm: UiElement<PlayerSide>
    {
        public RowVm AttackRow { get; set; }
        public RowVm DefenseRow { get; set; }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            AttackRow.Update(Model.GetAttackRow());
            DefenseRow.Update(Model.GetDefenseRow());
        }
    }
    
    public partial class TableVm: UiElement<Table>
    {
        public PlayerSideVm MySide { get; set; }
        public PlayerSideVm OpponentSide { get; set; }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            MySide.Update(Model.GetMySide());
            OpponentSide.Update(Model.GetOpponentSide());
        }
    }
    
    public partial class GameVm: UiElement<GameState>
    {
        public TableVm Table { get; set; }
        public HandVm MyHand { get; set; }

        protected override void OnUpdate()
        {
            Table.Update(Model.GetTable());
            MyHand.Update(Model.GetMyHand());
        }
    }
}