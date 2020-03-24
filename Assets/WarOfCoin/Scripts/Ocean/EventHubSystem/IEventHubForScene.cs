using System;
using System.Threading.Tasks;

namespace WarOfCoin.Scripts.Ocean.EventHubSystem {

    public interface IEventHubForScene {

        void Register<T>(Func<T, Task> topicHandler);

        Task Publish(object viewEvent);

    }

}