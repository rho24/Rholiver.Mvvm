namespace Rholiver.Mvvm.Infrastructure
{
    public interface IProvider<out TService, in TInput> where TService : ICanProcess<TInput>
    {
        TService GetFor(TInput input);
    }
}