using System.Windows;
using System.Windows.Data;
using Rholiver.Mvvm.Infrastructure;

namespace Rholiver.Mvvm.Views
{
    public interface IPropertyBinder : ICanProcess<FrameworkElement>
    {
        void BindIfNotAlready(FrameworkElement element, Binding binding);
    }
}