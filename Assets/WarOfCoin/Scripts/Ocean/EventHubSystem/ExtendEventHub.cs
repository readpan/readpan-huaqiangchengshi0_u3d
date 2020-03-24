using System;
using System.Threading.Tasks;

namespace WarOfCoin.Scripts.Ocean.EventHubSystem {

    public static class ExtendEventHub {

        public static IDisposable
            Subscribe<T>(this IEventHub sub, Action eventHandler) {
            return sub.Subscribe<T>(topic => {
                eventHandler();
                return Task.CompletedTask;
            });
        }

        public static IDisposable Subscribe<T>(this IEventHub sub,
            Action<T> eventHandler) {
            return sub.Subscribe<T>(topic => {
                eventHandler(topic);
                return Task.CompletedTask;
            });
        }

        public static void Register<T>(this IEventHubForScene sub,
            Action eventHandler) {
            sub.Register<T>(topic => {
                eventHandler();
                return Task.CompletedTask;
            });
        }

        public static void Register<T>(this IEventHubForScene sub,
            Action<T> eventHandler) {
            sub.Register<T>(topic => {
                eventHandler(topic);
                return Task.CompletedTask;
            });
        }

    }

}
