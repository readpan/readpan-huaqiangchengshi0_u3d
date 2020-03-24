using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WarOfCoin.Scripts.Ocean.Debug;

namespace WarOfCoin.Scripts.Ocean.PubSubSystem
{
    public interface IPub
    {
        Task Publish(object topic);
    }

    public interface ISub
    {
        void Subscribe(ISubscriber subscriber);
        void UnSubscribe(ISubscriber subscriber);
        IEnumerable<ISubscriber> GetAllHandler();
    }
    
    //订阅话题、发布话题
    public class PubSub : IPub, ISub
    {
        private readonly Sub sub = new Sub();

        public async Task Publish(object topic)
        {
            if (topic == null)
            {
                throw new ArgumentNullException($"{nameof(topic)}");
            }

            var type = topic.GetType();

            List<Task> tasks = new List<Task>();
            foreach (var subscriber in sub.GetHandlerList(type))
            {
                try
                {
                    tasks.Add(subscriber.HandleTopic(topic));
                }
                catch (Exception ex)
                {
                    Logger?.LogError($"Handle topic error: Subscriber: {subscriber}, Topic:{type}, error:{ex.Message}\n{ex.StackTrace}");
                }
            }

            if (tasks.Count == 0)
            {
                Logger?.LogWarning($"#PubSub# no handler found for {type}, should check your code.");
                return;
            }

            Logger?.Log($"#PubSub# Handle {type} with {tasks.Count} subscribers.");

            try
            {
                await Task.WhenAll(tasks);
            }
            catch (Exception ex)
            {
                Logger?.LogError($"Handle topic error: Topic:{type}, error:{ex.Message}\n{ex.StackTrace}");
                throw;
            }
            finally
            {
                Logger?.Log($"#PubSub# Handle {type} finish.");
            }
        }
        public void Subscribe(ISubscriber subscriber)
        {
            sub.Subscribe(subscriber);
            Logger?.Log($"#PubSub# Subscribe {subscriber}.");
        }
        public void UnSubscribe(ISubscriber subscriber)
        {
            sub.UnSubscribe(subscriber);
            Logger?.Log($"#PubSub# UnSubscribe {subscriber}.");
        }
        
        public IEnumerable<ISubscriber> GetAllHandler() => sub.GetAllHandler();
        
        public ILogger Logger { get; set; }
        
        private class Sub : ISub
        {
            protected Dictionary<Type, HashSet<ISubscriber>> handlers = new Dictionary<Type, HashSet<ISubscriber>>();
        
            public void Subscribe(ISubscriber subscriber)
            {
                if (subscriber == null) return;

                foreach (var type in subscriber.GetSupportTopics())
                {
                    if (!handlers.TryGetValue(type, out var list))
                    {
                        list = new HashSet<ISubscriber>();
                        handlers.Add(type, list);
                    }

                    list.Add(subscriber);
                }
            }

            public void UnSubscribe(ISubscriber subscriber)
            {
                foreach (var list in handlers.Values)
                {
                    list.Remove(subscriber);
                }
            }

            public IEnumerable<ISubscriber> GetHandlerList(Type topicType)
            {
                if (handlers.TryGetValue(topicType, out var list))
                {
                    return list;
                }
                return Enumerable.Empty<ISubscriber>();
            }
            
            public IEnumerable<ISubscriber> GetAllHandler()
            {
                return handlers.Values.SelectMany(list => list).Distinct().ToArray();
            }
        }
    }
}