using GameSetup.Api;
using UnityEngine;

namespace Ostium.View
{
    public class TableView: MonoBehaviour
    {
        [SerializeField]
        private RowView attackRow;
        [SerializeField]
        private RowView defenseRow;
        
        public void Init(Table table)
        {
            attackRow.Init(table.GetAttackRow());
            defenseRow.Init(table.GetDefenseRow());
        }
    }
}