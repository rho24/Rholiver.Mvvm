using System.Collections.ObjectModel;
using System.Windows;
using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Views;

namespace Rholiver.Mvvm.Navigation
{
    public class MultiElementManager : IMultiElementManager
    {
        private readonly IViewBuilder _viewBuilder;
        public ObservableCollection<UIElement> ElementValues { get; private set; }

        public MultiElementManager(IViewBuilder viewBuilder) {
            _viewBuilder = viewBuilder;
            ElementValues = new ObservableCollection<UIElement>();
        }

        public void Clear() {
            ElementValues.Clear();
        }

        public void Add<T>() where T : IViewModel {
            var modelAndView = _viewBuilder.Build<T>();

            ElementValues.Add(modelAndView.View);
        }

    }
}