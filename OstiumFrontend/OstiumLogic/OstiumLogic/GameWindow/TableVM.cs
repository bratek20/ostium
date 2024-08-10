using B20.Events.Api;
using B20.Frontend.Element;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class TableVM: ElementVM<Table>
    {
        public RowVM AttackRow { get; }
        public RowVM DefenseRow { get; }
        
        public TableVM(EventPublisher publisher)
        {
            AttackRow = new RowVM(RowType.ATTACK, publisher);
            DefenseRow = new RowVM(RowType.DEFENSE, publisher);
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            AttackRow.Update(Model.GetAttackRow());
            DefenseRow.Update(Model.GetDefenseRow());
        }
    }
}