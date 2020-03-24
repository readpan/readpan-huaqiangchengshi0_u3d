using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;

namespace WarOfCoin.Scripts.CoreGame.LevelData.Rule {

    public interface ICollectRule {

        CollectCoinResult Collect(LevelRuntimeData runtimeData, int collectColumn);

    }

}
