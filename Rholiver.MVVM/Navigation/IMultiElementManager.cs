using System.Collections.ObjectModel;
using System.Windows;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Navigation
{
    public interface IMultiElementManager
    {
        ObservableCollection<UIElement> ElementValues { get; }
        void Add<T>() where T : IViewModel;
    }
}