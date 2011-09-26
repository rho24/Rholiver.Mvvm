using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using Rholiver.Mvvm.Infrastructure;
using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Navigation;

namespace Rholiver.Mvvm.Views
{
    public class ViewWeaver : IViewWeaver
    {
        private readonly IProvider<IPropertyBinder, FrameworkElement> _binderProvider;
        private readonly IEnumerable<DefaultConverter> _defaultConverters;

        public ViewWeaver(IProvider<IPropertyBinder, FrameworkElement> binderProvider, IEnumerable<DefaultConverter> defaultConverters) {
            _binderProvider = binderProvider;
            _defaultConverters = defaultConverters;
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

                var binding = new Binding(element.Name) {
                                                            Mode = BindingMode.TwoWay,
                                                            Converter = _defaultConverters.Where(c => c.Type == property.PropertyType)
                                                                .Select(c => c.Converter).FirstOrDefault()
                                                        };


                var binder = _binderProvider.GetFor(element);

                binder.BindIfNotAlready(element, binding);
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

    public class DefaultConverter
    {
        public Type Type { get; set; }
        public IValueConverter Converter { get; set; }

        public DefaultConverter(Type type, IValueConverter converter) {
            Type = type;
            Converter = converter;
        }
    }

    public class MultiElementManagerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var manager = value as IMultiElementManager;

            if (manager == null)
                return value;

            return manager.ElementValues;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}