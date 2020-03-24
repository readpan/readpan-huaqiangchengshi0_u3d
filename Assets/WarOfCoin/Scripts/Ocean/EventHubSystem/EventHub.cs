using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;
using WarOfCoin.Scripts.Ocean.Debug;
using WarOfCoin.Scripts.Ocean.PubSubSystem;

namespace WarOfCoin.Scripts.Ocean.EventHubSystem {

    public class EventHub : IEventHub, IDisposable {

        public const string Tag = "EventHub";

        private readonly PubSub _pubSub;
        private readonly ILogger _logger;

        private readonly Dictionary<int, DisposableEventHub> _sceneHubs =
            new Dictionary<int, DisposableEventHub>();

        public EventHub(PubSub pubSub, ILogger logger = null) {
            _pubSub = pubSub;
            _logger = logger;
        }

        public void Dispose() {
            SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
            SceneManager.sceneUnloaded -= SceneManagerOnSceneUnloaded;
            foreach (var sceneHub in _sceneHubs.Values) {
                sceneHub.Dispose();
            }
        }

        public void RegisterUnitySceneEvent() {
            SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;
            SceneManager.sceneUnloaded += SceneManagerOnSceneUnloaded;
        }

        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode mode) {
            var sceneViewHub = new DisposableEventHub(this);
            foreach (var listener in scene.GetRootGameObjects()
                .SelectMany(root => root.GetComponentsInChildren<IEventListenerInScene>(true))) {
                _logger?.Log($"{Tag}, Register scene event listener: {listener.GetType().Name}");
                listener.RegisterEventHub(sceneViewHub);
            }
            var sceneKey = scene.buildIndex;
            DisposeListenerInScene(sceneKey);
            _sceneHubs[sceneKey] = sceneViewHub;
        }

        private void SceneManagerOnSceneUnloaded(Scene scene) {
            DisposeListenerInScene(scene.buildIndex);
        }

        private void DisposeListenerInScene(int sceneKey) {
            if (_sceneHubs.ContainsKey(sceneKey)) {
                _sceneHubs[sceneKey].Dispose();
                _sceneHubs.Remove(sceneKey);
            }
        }

        public async Task Publish(object eventData) {
            _logger?.Log($"{Tag}, Publish event: {eventData.GetType().Name}, {eventData}");
            await _pubSub.Publish(eventData);
        }

        public IDisposable Subscribe<T>(Func<T, Task> topicHandler) {
            return new UnitySubscriber<T>(_pubSub, topicHandler, _logger);
        }

    }

}
