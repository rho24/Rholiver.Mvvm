using System.Linq;
using Ninject;

namespace Rholiver.Mvvm.Infrastructure
{
    public class NinjectProvider<TService, TInput> : IProvider<TService, TInput> where TService : ICanProcess<TInput>
    {
        private readonly IKernel _kernel;

        public NinjectProvider(IKernel kernel) {
            _kernel = kernel;
        }

        public TService GetFor(TInput input) {
            var possibles = _kernel.GetAll<TService>();

            return possibles.Where(p => p.CanProcess(input)).FirstOrDefault();
        }
    }
}