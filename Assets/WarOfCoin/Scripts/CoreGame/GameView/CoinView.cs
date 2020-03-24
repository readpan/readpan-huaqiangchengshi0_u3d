using UnityEngine;
using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;

namespace WarOfCoin.Scripts.CoreGame.GameView {

    public class CoinView : MonoBehaviour {

        [SerializeField]
        private GameObject[] coinPrefab;

        [SerializeField]
        private Transform coinParent;

        public void SetCoinView(Coin coin) {
            Instantiate(coinPrefab[(int) coin.Type], coinParent);
        }

    }

}
