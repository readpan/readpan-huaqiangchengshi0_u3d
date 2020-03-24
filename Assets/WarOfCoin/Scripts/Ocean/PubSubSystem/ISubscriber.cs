using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WarOfCoin.Scripts.Ocean.PubSubSystem
{
    public interface ISubscriber
    {
        Task HandleTopic(object topic);
        IEnumerable<Type> GetSupportTopics();
    }
    
    public interface ISubscriber<in T> : ISubscriber
    {
        Task HandleTopic(T topic);
    }
}