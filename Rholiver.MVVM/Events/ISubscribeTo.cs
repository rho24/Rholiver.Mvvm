namespace Rholiver.Mvvm.Events
{
    public interface ISubscribeTo<in T> : ISubscriber where T : IMessage
    {
        void OnMessage(T message);
    }

    public interface ISubscriber
    {}

    public interface IEventAggregator
    {
        void SendMessage<T>(T message) where T : IMessage;
        void Subscribe(ISubscriber subscriber);
        void Unsubscribe(ISubscriber subscriber);
    }
}