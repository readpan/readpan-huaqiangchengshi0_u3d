using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarOfCoin.Scripts.Ocean.Debug;
using WarOfCoin.Scripts.Ocean.PubSubSystem;

namespace WarOfCoin.Scripts.Ocean.EventHubSystem {

    internal struct UnitySubscriber<T> : ISubscriber<T>, IDisposable {

        private readonly Func<T, Task> _action;
        private readonly ISub _sub;
        private readonly ILogger _logger;

        public UnitySubscriber(ISub sub, Func<T, Task> action, ILogger logger = null) {
            _action = action;
            _sub = sub;
            _logger = logger;
            _sub.Subscribe(this);
            _logger?.Log($"{EventHub.Tag}, Subscribe event: {typeof(T).Name}");
        }

        /// <summary>
        /// 必须使用await，以让unity在主线程中运行action
        /// </summary>
        public async Task HandleTopic(T topic) {
            await _action(topic);
        }

        public Task HandleTopic(object topic) {
            return HandleTopic((T) topic);
        }

        public IEnumerable<Type> GetSupportTopics() {
            yield return typeof(T);
        }

        public void Dispose() {
            _sub.UnSubscribe(this);
            _logger?.Log($"{EventHub.Tag}, UnSubscribe event: {typeof(T).Name}");
        }

    }

}