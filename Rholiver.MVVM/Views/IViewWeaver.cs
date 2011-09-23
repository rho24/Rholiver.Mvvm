using System.Windows;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Views
{
    public interface IViewWeaver
    {
        void Weave(UIElement view, IViewModel viewModel);
    }
}