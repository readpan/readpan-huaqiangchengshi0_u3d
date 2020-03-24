using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using WarOfCoin.Scripts.CoreGame.LevelData.Event;
using WarOfCoin.Scripts.Ocean.EventHubSystem;

namespace WarOfCoin.Scripts.CoreGame.GameView {

    public class InputView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IEventListenerInScene {

        private Vector2 _beginDragPosition;

        [SerializeField]
        private float legalDragDistance;

        private IEventHubForScene _eventHub;

        public void OnBeginDrag(PointerEventData eventData) {
            _beginDragPosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData) {
        }

        public void OnEndDrag(PointerEventData eventData) {
            var endPosition = eventData.position;
            var distance = endPosition.y - _beginDragPosition.y;
            if (Mathf.Abs(distance) < legalDragDistance) {
                return;
            }
            var gameInput = new GameInput {
                InputArea = Camera.main.ScreenToWorldPoint(_beginDragPosition),
                InputType = distance > 0 ? InputType.SwapUp : InputType.SwapDown
            };
            _eventHub.Publish(gameInput);
        }

        public void RegisterEventHub(IEventHubForScene hub) {
            Debug.Log(transform.position);
            _eventHub = hub;
            _eventHub.Register<GameInput>(OnGamInput);
        }

        private Task OnGamInput(GameInput arg) {
            Debug.Log($"作用位置{arg.InputArea}");
            Debug.Log($"输入类型:{arg.InputType.ToString()}");
            return Task.CompletedTask;
        }

    }

}
