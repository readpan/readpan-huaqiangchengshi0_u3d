using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WarOfCoin.Scripts.Ocean.EventHubSystem {

    public class DisposableEventHub : IEventHubForScene, IDisposable {

        private readonly List<IDisposable> _disposables = new List<IDisposable>();
        private IEventHub _eventHub;

        public DisposableEventHub(IEventHub eventHub) {
            _eventHub = eventHub;
        }

        public void Register<T>(Func<T, Task> topicHandler) {
            _disposables.Add(_eventHub.Subscribe(topicHandler));
        }

        public Task Publish(object viewEvent) {
            return _eventHub?.Publish(viewEvent);
        }

        public void Dispose() {
            _disposables.ForEach(d => d.Dispose());
            _disposables.Clear();
            _eventHub = null;
        }

    }

}
