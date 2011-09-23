namespace Rholiver.Mvvm.Events
{
    public interface IMessage
    {
        object Sender { get; }
    }

    public interface IMessage<out T> : IMessage
    {
        T Value { get; }
    }
}