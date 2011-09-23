namespace Rholiver.Mvvm.Infrastructure
{
    public interface ICanProcess<in T>
    {
        bool CanProcess(T input);
    }
}