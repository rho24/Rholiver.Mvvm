using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;
using Rholiver.Mvvm.Events;
using Rholiver.Mvvm.Infrastructure;

namespace Rholiver.Mvvm
{
    public class EventAggregator : IEventAggregator
    {
        private readonly Dispatcher _dispatcher;
        private readonly ICollection<ISubscriber> _subscribers;

        public EventAggregator(Dispatcher dispatcher) {
            _dispatcher = dispatcher;
            _subscribers = new WeakCollection<ISubscriber>();
        }

        public void SendMessage<T>(T message) where T : IMessage {
            foreach (var subscriber in _subscribers.OfType<ISubscribeTo<T>>()) {
                var localSubscriber = subscriber;
                _dispatcher.BeginInvoke(() => localSubscriber.OnMessage(message));
            }
        }

        public void Subscribe(ISubscriber subscriber) {
            _subscribers.Add(subscriber);
        }

        public void Unsubscribe(ISubscriber subscriber) {
            _subscribers.Remove(subscriber);
        }
    }
}