using UnityEngine;
using WarOfCoin.Scripts.CoreGame.LevelData.DataStructure;
using WarOfCoin.Scripts.Ocean.EventHubSystem;

namespace WarOfCoin.Scripts.CoreGame.GameView {

    public class LevelMapView : MonoBehaviour, IEventListenerInScene {

        [SerializeField]
        private CoinView coinViewPrefab;

        [Range(1, 100)]
        public float intervalMultiple = 1;

        public void RegisterEventHub(IEventHubForScene hub) {
            hub.Register<LevelMap>(UpdateData);
        }

        private void UpdateData(LevelMap data) {    
            foreach (Coin[] coins in data.Coins) {
                foreach (Coin coin in coins) {
                    var go = Instantiate(coinViewPrefab, transform);
                    go.SetCoinView(coin);
                    go.transform.localPosition =
                        new Vector3(coin.Coordinate.x, coin.Coordinate.y) * intervalMultiple;
                }
            }
        }

    }

}
