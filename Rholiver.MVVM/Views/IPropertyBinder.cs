using System.Windows;
using System.Windows.Data;
using Rholiver.Mvvm.Infrastructure;

namespace Rholiver.Mvvm.Views
{
    public interface IPropertyBinder : ICanProcess<FrameworkElement>
    {
        void Bind(FrameworkElement element, Binding binding);
    }

    internal class ControlBinder<T> : IPropertyBinder where T : FrameworkElement
    {
        public bool CanProcess(FrameworkElement input) {
            return input is T;
        }

        public void Bind(FrameworkElement element, Binding binding) {
            BindingOperations.SetBinding(element, ControlProperty, binding);
        }

        public DependencyProperty ControlProperty { get; set; }
    }
}