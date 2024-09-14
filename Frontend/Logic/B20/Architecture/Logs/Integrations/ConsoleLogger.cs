using System;
using B20.Architecture.Logs.Api;

namespace B20.Architecture.Logs.Integrations
{
    public class ConsoleLogger: Logger
    {
        public void Info(string message)
        {
            Console.Out.Write("[INFO] " + message);
        }

        public void Warn(string message)
        {
            Console.Out.Write("[WARN] " + message);
        }

        public void Error(string message)
        {
            Console.Out.Write("[ERROR] " + message);
        }
    }
}