using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace WarOfCoin.Scripts.Ocean.PubSubSystem
{
    public interface IPubQueue
    {
        Task AddToTail(object topic);
        Task AddToHead(object topic);
        void SetEnable(bool enable);
    }
    
    //通过队列逐个发布话题
    public class PubQueue : IPubQueue
    {
        private IPub pub;
        private LinkedList<WaitableTopic> viewModelQueue = new LinkedList<WaitableTopic>();
        
        public PubQueue(IPub pub)
        {
            this.pub = pub;
            SetEnable(true);
        }

        //add msg to queue's tail
        public async Task AddToTail(object topic)
        {
            var wt = new WaitableTopic(topic);
            viewModelQueue.AddLast(wt);
            CheckRunning();
            await wt;
        }
        
        //add msg to queue's head
        public async Task AddToHead(object topic)
        {
            var wt = new WaitableTopic(topic);
            viewModelQueue.AddFirst(wt);
            CheckRunning();
            await wt;
        }

        public bool IsEnabled { get; private set; }

        public void SetEnable(bool enable)
        {
            IsEnabled = enable;
            if(enable) CheckRunning();
        }

        private bool isWorking;
        private async void CheckRunning()
        {
            if (isWorking) return;
            
            isWorking = true;
            while (IsEnabled && viewModelQueue.Count > 0)
            {
                var wt = viewModelQueue.First.Value;
                viewModelQueue.RemoveFirst();
                try
                {
                    await pub.Publish(wt.Topic);
                }
                catch
                {
                    // ignored, but must exists. see https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/try-finally
                }
                finally
                {
                    wt.Complete();
                }
            }

            isWorking = false;
        }
        
        private class WaitableTopic : ICriticalNotifyCompletion
        {
            public object Topic;

            public WaitableTopic(object topic)
            {
                this.Topic = topic;
                IsCompleted = false;
            }

            public void Complete()
            {
                callback?.Invoke();
            }
            
            public WaitableTopic GetAwaiter()
            {
                return this;
            }

            [ExcludeFromCodeCoverage]
            public void OnCompleted(Action continuation)
            {
                callback = continuation;
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                callback = continuation;
            }

            private Action callback;
            
            public bool IsCompleted { get; }

            public object GetResult()
            {
                return Topic;
            }
        }
    }

    public static class PubSubExtension
    {
        public static IPubQueue AddPubQueue(this IPub pub) => new PubQueue(pub);
    }
}