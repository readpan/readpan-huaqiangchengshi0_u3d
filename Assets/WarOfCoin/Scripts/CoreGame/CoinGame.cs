using System;
using System.IO;
using UnityEngine;
using WarOfCoin.Scripts.CoreGame.Config;
using WarOfCoin.Scripts.CoreGame.Controller;
using WarOfCoin.Scripts.CoreGame.LevelData;
using WarOfCoin.Scripts.Ocean.EventHubSystem;

namespace WarOfCoin.Scripts.CoreGame {

    public class CoinGame : IDisposable {

        private readonly IEventHub _eventHub;
        private readonly Action _returnCallback;
        private CoinGameLevelController _levelController;


        public CoinGame(IEventHub eventHub, Action returnCallback) {
            _eventHub = eventHub;
            _returnCallback = returnCallback;
        }

        public void Dispose() {
            _levelController?.Dispose();
        }

        public void StartGame() {
            var level = 1;
            var config =
                Resources.Load<LevelConfigObject>(Path.Combine("CoinGameLevel", level.ToString()))
                    .levelConfig;
            _levelController?.Dispose();
            _levelController = new CoinGameLevelController(config, _eventHub,
                new RandomGenerator(1), (restartGame) => {
                    _levelController?.Dispose();
                    _levelController = null;
                    if (restartGame) {
                        StartGame();
                    } else {
                        _returnCallback();
                    }
                });
            
            _levelController.StartGame();
        }

    }

}
