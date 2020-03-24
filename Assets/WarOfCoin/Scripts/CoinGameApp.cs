using System;
using WarOfCoin.Scripts.CoreGame;
using WarOfCoin.Scripts.Ocean.Debug;
using WarOfCoin.Scripts.Ocean.EventHubSystem;
using WarOfCoin.Scripts.Ocean.PubSubSystem;

namespace WarOfCoin.Scripts {

    public class CoinGameApp : IDisposable {

        private readonly EventHub _eventHub;
        private CoinGame _currentGame;

        public CoinGameApp(PubSub pubSub, ILogger logger) {
            _eventHub = new EventHub(pubSub, logger);
        }

        public void Dispose() {
            _eventHub?.Dispose();
            _currentGame?.Dispose();
        }

        /// <summary>
        /// 初始化入口,只需要执行一次,需要在初始场景中调用。
        /// </summary>
        public void Init() {
            _eventHub.RegisterUnitySceneEvent();
        }

        public void StartGame() {
            _currentGame?.Dispose();
            _currentGame = new CoinGame(_eventHub, null);
            _currentGame.StartGame();
        }

    }

}
