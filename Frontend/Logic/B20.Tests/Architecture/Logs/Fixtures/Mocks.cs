using System.Collections.Generic;
using B20.Architecture.Logs.Api;
using Xunit;

namespace B20.Architecture.Logs.Fixtures
{
    public class LoggerMock : Logger
    {
        private readonly List<string> infos = new List<string>();
        private readonly List<string> warns = new List<string>();
        private readonly List<string> errors = new List<string>();

        public void Info(string message)
        {
            infos.Add(message);
        }

        public void Warn(string message)
        {
            warns.Add(message);
        }

        public void Error(string message)
        {
            errors.Add(message);
        }

        public void AssertInfos(params string[] messages)
        {
            Assert.Equal(messages, infos.ToArray());
        }

        public void AssertWarns(params string[] messages)
        {
            Assert.Equal(messages, warns.ToArray());
        }

        public void AssertErrors(params string[] messages)
        {
            Assert.Equal(messages, errors.ToArray());
        }

        public void AssertNoInfos()
        {
            Assert.Empty(infos);
        }

        public void AssertNoWarns()
        {
            Assert.Empty(warns);
        }

        public void AssertNoErrors()
        {
            Assert.Empty(errors);
        }

        public void Reset()
        {
            infos.Clear();
            warns.Clear();
            errors.Clear();
        }
    }

}