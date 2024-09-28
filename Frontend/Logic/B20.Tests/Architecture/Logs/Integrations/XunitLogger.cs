using B20.Architecture.Logs.Api;
using Xunit.Abstractions;

namespace B20.Tests.Architecture.Logs.Integrations
{
    public class XunitLogger: Logger
    {
        private readonly ITestOutputHelper output;
        
        public XunitLogger(ITestOutputHelper output)
        {
            this.output = output;
        }

        public void Info(string message)
        {
            output.WriteLine("[INFO] " + message);
        }

        public void Warn(string message)
        {
            output.WriteLine("[WARN] " + message);
        }

        public void Error(string message)
        {
            output.WriteLine("[ERROR] " + message);
        }
    }
}