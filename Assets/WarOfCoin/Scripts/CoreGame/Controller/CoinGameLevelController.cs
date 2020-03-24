using System;
using System.Threading.Tasks;
using WarOfCoin.Scripts.CoreGame.Config;
using WarOfCoin.Scripts.CoreGame.LevelData;
using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;
using WarOfCoin.Scripts.CoreGame.LevelData.Event;
using WarOfCoin.Scripts.CoreGame.LevelData.Rule;
using WarOfCoin.Scripts.Ocean.EventHubSystem;

namespace WarOfCoin.Scripts.CoreGame.Controller {

    public class CoinGameLevelController : IDisposable {

        private readonly DisposableEventHub _eventHub;
        private readonly RandomGenerator _random;
        private readonly Level _level;
        private readonly Action<bool> _returnAction;

        private ICollectRule _collectRule;
        private IReleaseCoinRule _releaseCoinRule;
        private IGameOverRule _gameOverRule;
        
        private LevelRuntimeData _levelRuntimeData;

        public CoinGameLevelController(LevelConfig config, IEventHub eventHub,
            RandomGenerator randomGenerator, Action<bool> returnAction) {
            _eventHub = new DisposableEventHub(eventHub);
            _random = randomGenerator;
            _level = config.CreateLevel(_random);
            _returnAction = returnAction;
            GenerateRules();
            
            _eventHub.Register<GameInput>(OnGameInput);
        }

        private void OnGameInput(GameInput gameInput) {
            //查询滑动影响的列序号
        }

        private void GenerateRules() {
            _collectRule = new CoinCollectRule();
            _releaseCoinRule = new ReleaseCoinRule();
            _gameOverRule = new GameOverRule();
        }

        public void Dispose() {
            _eventHub.Dispose();
        }

        public void StartGame() {
            _levelRuntimeData = LevelRuntimeData.CreateInitData(_level, _random);
            _eventHub.Publish(_level.LevelMap);
//            _collectRule.Collect(_levelRuntimeData,)
        }

    }

}
