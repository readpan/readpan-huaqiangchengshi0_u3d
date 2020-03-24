using System.Diagnostics.CodeAnalysis;

namespace WarOfCoin.Scripts.Ocean.Debug
{
    [ExcludeFromCodeCoverage]
    public class NullLogger : ILogger
    {
        public void Log(string message)
        {
        }

        public void LogError(string message)
        {
        }

        public void LogWarning(string message)
        {
        }
    }
}
