using System;
using System.Collections.Generic;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using B20.Frontend.Traits;
using GameModule.Api;

namespace GameModule.ViewModel
{
    public partial class CreatureCardVm: ElementVm<CreatureCard>
    {
        public LabelVm Name { get; } = new LabelVm();
        public VisibleVm Selected { get; } = new VisibleVm();
        
        protected override List<Type> GetTraitTypes()
        {
            return new List<Type>() {typeof(WithPosition2d), typeof(Draggable)};
        }
        
        protected override void OnUpdate()
        {
            Name.Update(Model.GetId().Value);
        }
    }
    
    public class CreateCardListVm: ElementListVm<CreatureCardVm, CreatureCard>
    {
        public CreateCardListVm(B20.Architecture.Contexts.Api.Context c) : base(() => c.Get<CreatureCardVm>())
        {
            
        }
    }
    
    public partial class HandVm: ElementVm<Hand>
    {
        public CreateCardListVm Cards { get; set; }

        protected override void OnUpdate()
        {
            Cards.Update(Model.GetCards());
        }
    }
    
    public class OptionalCreatureCardVm: OptionalElementVm<CreatureCardVm, CreatureCard>
    {
        public OptionalCreatureCardVm(CreatureCardVm element) : base(element)
        {
        }
    }
    
    public partial class RowVm: ElementVm<Row>
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
    
    public partial class TableVm: ElementVm<Table>
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
    
    public partial class GameVm: ElementVm<Game>
    {
        public TableVm Table { get; set;  }
        public HandVm Hand { get; set; }

        protected override void OnUpdate()
        {
            Table.Update(Model.GetTable());
            Hand.Update(Model.GetHand());
        }
    }
}