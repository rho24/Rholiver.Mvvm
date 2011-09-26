using System;
using System.Windows;
using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Views;

namespace Rholiver.Mvvm.Navigation
{
    public class ElementManager : IElementManager
    {
        private readonly IViewBuilder _viewBuilder;
        private Action _onChange;

        public ElementManager(IViewBuilder viewBuilder) {
            _viewBuilder = viewBuilder;
        }

        public void Initialize(Action onChange) {
            _onChange = onChange;
        }

        public void Clear() {
            ElementValue = null;
            _onChange();
        }

        public UIElement ElementValue { get; private set; }

        public void NavigateTo<T>() where T : IViewModel {
            var modelAndView = _viewBuilder.Build<T>();

            ElementValue = modelAndView.View;
            _onChange();
        }
    }
}