using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;

namespace WarOfCoin.Scripts.CoreGame.LevelData.Rule {

    public interface IReleaseCoinRule {

        RemoveResult MatchAndRemove(LevelRuntimeData runtimeData, int releaseColumn);

    }

}
