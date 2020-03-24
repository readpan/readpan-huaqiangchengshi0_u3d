using System;
using UnityEngine;
using WarOfCoin.Scripts.CoreGame.Config;
using Random = UnityEngine.Random;

namespace WarOfCoin.Scripts.CoreGame.LevelData.DataStructure {

    public struct LevelMap {

        //零用钱数组
        public Coin[][] Coins;

        public static LevelMap CreateLevelMap(int width, int height) {
            LevelMap levelMap = new LevelMap();
            Coin[][] coins = new Coin[height][];
            for (int i = 0; i < height; i++) {
                coins[i] = new Coin[width];
                for (int j = 0; j < width; j++) {
                    var typeInt = Random.Range(1, Enum.GetNames(typeof(CoinType)).Length);
                    coins[i][j] = new Coin {
                        Type = (CoinType) typeInt,
                        Coordinate = new Vector2Int(j, i),
                    };
                }
            }
            levelMap.Coins = coins;

            return levelMap;
        }

        public static LevelMap CreateLevelMap(LevelMapConfig levelMapConfig,
            RandomGenerator randomGenerator) {
            var height = levelMapConfig.mapUnitConfigArrays.Length;
            var width = levelMapConfig.mapUnitConfigArrays.Length > 0
                ? levelMapConfig.mapUnitConfigArrays[0].levelMapUnitConfigs.Length
                : 0;
            LevelMap levelMap = new LevelMap();
            Coin[][] coins = new Coin[height][];
            for (int i = 0; i < height; i++) {
                coins[i] = new Coin[width];
                for (int j = 0; j < width; j++) {
                    var typeInt = randomGenerator.GetRandomNum(1, Enum.GetNames(typeof(CoinType)).Length);
                    coins[i][j] = new Coin {
                        Type = (CoinType) typeInt,
                        Coordinate = new Vector2Int(j, i),
                    };
                }
            }
            levelMap.Coins = coins;

            return levelMap;
        }

    }

}
