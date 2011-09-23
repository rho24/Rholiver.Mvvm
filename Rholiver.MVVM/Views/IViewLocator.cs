using System.Windows;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Views
{
    public interface IViewLocator
    {
        UIElement GetView(IViewModel viewModel);
    }
}