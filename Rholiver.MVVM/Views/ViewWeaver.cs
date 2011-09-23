using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Rholiver.Mvvm.Infrastructure;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Views
{
    public class ViewWeaver : IViewWeaver
    {
        private readonly IProvider<IPropertyBinder, FrameworkElement> _binderProvider;

        public ViewWeaver(IProvider<IPropertyBinder, FrameworkElement> binderProvider) {
            _binderProvider = binderProvider;
        }

        public void Weave(UIElement view, IViewModel viewModel) {
            BindContext(view, viewModel);

            var namedElements = (view is ChildWindow ? GetChildWindowNamedElements(view) : GetNamedElements(view)).ToList();
            BindProperties(namedElements, viewModel);
            BindMethods(namedElements, viewModel);
        }

        private void BindContext(UIElement view, IViewModel viewModel) {
            var frameworkElement = view as FrameworkElement;
            if (frameworkElement != null)
                frameworkElement.DataContext = viewModel;
        }

        private void BindProperties(IEnumerable<FrameworkElement> elements, IViewModel viewModel) {
            var properties = viewModel.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var element in elements) {
                var property = properties.Where(p => p.Name == element.Name).SingleOrDefault();

                if (property == null)
                    continue;

                var binding = new Binding(element.Name) {Mode = BindingMode.TwoWay};

                var binder = _binderProvider.GetFor(element);

                binder.Bind(element, binding);
            }
        }

        private void BindMethods(IEnumerable<FrameworkElement> elements, IViewModel viewModel) {
            var methods = viewModel.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);

            foreach (var element in elements) {
                var method = methods.Where(m => m.Name == element.Name).SingleOrDefault();

                if (method == null)
                    continue;

                var command =
                    new DelegateCommand(
                        parameter => method.Invoke(viewModel, parameter == null ? null : new[] {parameter}),
                        parameter => true);

                element.SetValue(ButtonBase.CommandProperty, command);
            }
        }

        private IEnumerable<FrameworkElement> GetChildWindowNamedElements(UIElement view) {
            var childWindow = (ChildWindow) view;

            return GetNamedElements((UIElement) childWindow.Content);
        }

        private IEnumerable<FrameworkElement> GetNamedElements(UIElement view) {
            var root = view as DependencyObject;
            var count = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; i++) {
                var child = VisualTreeHelper.GetChild(root, i);
                var childElement = child as FrameworkElement;
                if (childElement != null) {
                    if (!string.IsNullOrEmpty(childElement.Name))
                        yield return childElement;
                }
                var innerChildren = GetNamedElements(childElement);
                foreach (var innerChild in innerChildren) yield return innerChild;
            }

            yield break;
        }
    }
}