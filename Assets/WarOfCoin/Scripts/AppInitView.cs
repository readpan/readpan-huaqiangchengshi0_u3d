using UnityEngine;
using WarOfCoin.Scripts;
using WarOfCoin.Scripts.Ocean.PubSubSystem;
using ILogger = WarOfCoin.Scripts.Ocean.Debug.ILogger;

namespace DefaultNamespace {

    public class AppInitView : UnityEngine.MonoBehaviour {

        private CoinGameApp _app;

        private void Awake() {
            ILogger logger = null;
            if (Application.isEditor) {
                logger = new UnityLogger();
            }
            var pubSub = new PubSub();
            _app = new CoinGameApp(pubSub, logger);
            _app.Init();
        }

        private void Start() {
            _app.StartGame();
        }

        private class UnityLogger : ILogger {

            public void Log(string message) {
                Debug.Log(message);
            }

            public void LogWarning(string message) {
                Debug.LogWarning(message);
            }

            public void LogError(string message) {
                Debug.LogError(message);
            }

        }
    }
    
    

}
