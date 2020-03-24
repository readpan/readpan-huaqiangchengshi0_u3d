using System;
using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;

namespace WarOfCoin.Scripts.CoreGame.Config {

    [Serializable]
    public class CoinConfig {

        public CoinConfigType coinConfigType = CoinConfigType.Random;
        public CoinType coinType = CoinType.None;

    }

}
