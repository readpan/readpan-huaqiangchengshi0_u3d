using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;
using WarOfCoin.Scripts.CoreGame.LevelData.Results;

namespace WarOfCoin.Scripts.CoreGame.LevelData.Rule {

    public interface IGameOverRule {

        GameResult GetGameResult(LevelRuntimeData runtimeData, Level level, long currentMilliSeconds);

    }

}
