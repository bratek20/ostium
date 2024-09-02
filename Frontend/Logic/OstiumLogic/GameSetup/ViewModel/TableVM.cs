using B20.Events.Api;
using B20.Frontend.Element;
using GameSetup.Api;

namespace Ostium.Logic
{
    public partial class TableVM: ElementVm<Table>
    {
        public RowVM AttackRow { get; }
        public RowVM DefenseRow { get; }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            AttackRow.Update(Model.GetAttackRow());
            DefenseRow.Update(Model.GetDefenseRow());
        }
    }

    public partial class TableVM
    {
        public TableVM(EventPublisher publisher)
        {
            AttackRow = new RowVM(RowType.ATTACK, publisher);
            DefenseRow = new RowVM(RowType.DEFENSE, publisher);
        }
    }
}