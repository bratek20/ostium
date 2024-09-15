namespace B20.Architecture.Logs.Api
{
    public interface Logger
    {
        void Info(string message);
        void Warn(string message);
        void Error(string message);
    }
}