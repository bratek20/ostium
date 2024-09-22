using UnityEngine;
using Logger = B20.Architecture.Logs.Api.Logger;

namespace B20.Architecture.Logs.Integrations
{
    public class UnityLogger: Logger
    {
        public void Info(string message)
        {
            Debug.Log("[INFO] " + message);
        }

        public void Warn(string message)
        {
            Debug.LogWarning("[WARN] " + message);
        }

        public void Error(string message)
        {
            Debug.LogError("[ERROR] " + message);
        }
    }
}