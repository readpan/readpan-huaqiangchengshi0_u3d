using WarOfCoin.Scripts.CoreGame.LevelData;
using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;

namespace WarOfCoin.Scripts.CoreGame.Config {

    public static class LevelMapCreator {

        public static LevelMap CreateLevelMap(LevelConfig levelConfig,
            RandomGenerator randomGenerator) {
            return LevelMap.CreateLevelMap(levelConfig.levelMapConfig, randomGenerator);
        }

    }

}
