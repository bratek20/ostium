using B20.Architecture.Contexts.Context;
using B20.Architecture.Logs.Context;
using B20.Frontend.Timer.Api;
using B20.Frontend.Windows.Api;
using B20.Frontend.Windows.Integrations;
using Ostium.Logic;
using UnityEngine;

namespace SingleGame.View
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private UnityWindowManipulator windowManipulator;

        private OstiumLogic logic;
        private TimerApi timerApi;
        
        void Start()
        {
            var c = ContextsFactory.CreateBuilder()
                .SetImplObject<WindowManipulator>(windowManipulator)
                .WithModules(
                    new UnityLogsImpl(),
                    new OstiumLogicFullImpl()
                )
                .Build();

            timerApi = c.Get<TimerApi>();
            
            logic = c.Get<OstiumLogic>();
            logic.Start();
        }
        
        void Update()
        {
            int deltaMs = (int)(Time.deltaTime * 1000);
            timerApi.Progress(deltaMs);
        }
    }
}

