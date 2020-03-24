namespace WarOfCoin.Scripts.Ocean.Debug
{
    public interface ILogger
    {
        void Log(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}