using Ninject;
using Ninject.Activation;
using Ninject.Activation.Strategies;
using Rholiver.Mvvm.Events;

namespace Rholiver.Mvvm
{
    public class SubcribeToEventAggregatorStrategy : IActivationStrategy
    {
        public INinjectSettings Settings { get; set; }

        public void Activate(IContext context, InstanceReference reference) {
            reference.IfInstanceIs<ISubscriber>(s => {
                                                    var eventAggregator = context.Kernel.Get<IEventAggregator>();
                                                    eventAggregator.Subscribe(s);
                                                });
        }

        public void Deactivate(IContext context, InstanceReference reference) {
            reference.IfInstanceIs<ISubscriber>(s => {
                                                    var eventAggregator = context.Kernel.Get<IEventAggregator>();
                                                    eventAggregator.Unsubscribe(s);
                                                });
        }

        public void Dispose() {}
    }
}