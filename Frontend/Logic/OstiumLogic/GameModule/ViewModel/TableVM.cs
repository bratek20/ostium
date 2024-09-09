using B20.Events.Api;
using B20.Frontend.Element;
using GameModule.Api;


namespace Ostium.Logic
{
    public partial class TableVM: ElementVm<Table>
    {
        public RowVM AttackRow { get; set; }
        public RowVM DefenseRow { get; set; }

        protected override void OnUpdate()
        {
            base.OnUpdate();
            AttackRow.Update(Model.GetAttackRow());
            DefenseRow.Update(Model.GetDefenseRow());
        }
    }
}