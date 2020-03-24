using System;
using System.Threading.Tasks;

namespace WarOfCoin.Scripts.Ocean.EventHubSystem {

    public interface IEventHub {

        IDisposable Subscribe<T>(Func<T, Task> eventHandler);

        Task Publish(object viewEvent);
    }

}