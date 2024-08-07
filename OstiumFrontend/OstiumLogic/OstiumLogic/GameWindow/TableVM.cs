using B20.Events.Api;
using B20.Frontend.Elements;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class TableVM: ElementVM<Table>
    {
        public RowVM AttackRow { get; private set; }
        public RowVM DefenseRow { get; private set; }
        
        public TableVM(EventPublisher publisher)
        {
            AttackRow = new RowVM(publisher);
            DefenseRow = new RowVM(publisher);
        }
        
        protected override void OnUpdate()
        {
            base.OnUpdate();
            AttackRow.Update(Model.GetAttackRow());
            DefenseRow.Update(Model.GetDefenseRow());
        }
    }
}