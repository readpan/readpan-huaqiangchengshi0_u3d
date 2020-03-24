using System;
using WarOfCoin.Scripts.CoreGame.LevelData;
using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;
using WarOfCoin.Scripts.CoreGame.LevelData.Rule;

namespace WarOfCoin.Scripts.CoreGame.Config {

    [Serializable]
    public class LevelConfig {

        public LevelMapConfig levelMapConfig;

        public LevelRule levelRule;

        public Level CreateLevel(RandomGenerator randomGenerator) {
            return new Level {
                LevelMap = LevelMapCreator.CreateLevelMap(this, randomGenerator),
                LevelRule = levelRule
            };
        }

    }

}
