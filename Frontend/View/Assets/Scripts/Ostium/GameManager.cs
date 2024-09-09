using B20.Architecture.Contexts.Context;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Integrations;
using Ostium.Logic;
using UnityEngine;

namespace Ostium.View
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private UnityWindowManipulator windowManipulator;

        private OstiumLogic logic;
        
        void Start()
        {
            var c = ContextsFactory.CreateBuilder()
                .SetImplObject<WindowManipulator>(windowManipulator)
                .WithModule(new OstiumLogicFullImpl())
                .Build();

            logic = c.Get<OstiumLogic>();
            logic.Start();
        }
    }
}

