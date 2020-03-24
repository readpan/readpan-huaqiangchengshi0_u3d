using WarOfCoin.Scripts.CoreGame.LevelData.Event;
using WarOfCoin.Scripts.CoreGame.LevelData.Results;

namespace WarOfCoin.Scripts.CoreGame.LevelData.DataStructure {

    public class LevelRuntimeData {

        public LevelMap LevelMap;

        public GameStart Start;

        public GameResult Result;

        public RandomGenerator RandomGenerator;

        public CoinsInHand CoinInHand;
        
        public static LevelRuntimeData CreateInitData(Level level, RandomGenerator randomGenerator) {
            return new LevelRuntimeData() {
                LevelMap = level.LevelMap,
                Start = new GameStart(),
                Result = new GameResult(),
                RandomGenerator = randomGenerator,
            };
        }
        

    }

    public struct CoinsInHand {

        public Coin[] Coins;
        
    }
    
    

}
