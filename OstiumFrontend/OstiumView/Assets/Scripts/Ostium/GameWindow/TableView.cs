using B20.View;
using GameSetup.Api;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class TableView: ElementView<TableVM>
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