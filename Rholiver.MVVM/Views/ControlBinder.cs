using System.Windows;
using System.Windows.Data;

namespace Rholiver.Mvvm.Views
{
    internal class ControlBinder<T> : IPropertyBinder where T : FrameworkElement
    {
        public DependencyProperty ControlProperty { get; set; }

        public bool CanProcess(FrameworkElement input) {
            return input is T;
        }

        public void BindIfNotAlready(FrameworkElement element, Binding binding) {
            BindingOperations.SetBinding(element, ControlProperty, binding);
        }
    }
}