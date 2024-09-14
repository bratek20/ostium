using B20.Frontend.Elements.View;
using GameModule.Api;
using GameModule.ViewModel;
using Ostium.Logic;
using UnityEngine;

namespace GameModule.View
{
    public class TableView: ElementView<TableVm>
    {
        [SerializeField]
        private RowView attackRow;
        [SerializeField]
        private RowView defenseRow;

        protected override void OnBind()
        {
            base.OnBind();
        
            attackRow.Bind(ViewModel.AttackRow);
            defenseRow.Bind(ViewModel.DefenseRow);
        }
    }
}