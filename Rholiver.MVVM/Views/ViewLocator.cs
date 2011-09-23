using System;
using System.Linq;
using System.Reflection;
using System.Windows;
using Ninject;
using Rholiver.Mvvm.Infrastructure;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Views
{
    public class ViewLocator : IViewLocator
    {
        private readonly IKernel _kernel;

        public ViewLocator(IKernel kernel) {
            _kernel = kernel;
        }

        public UIElement GetView(IViewModel viewModel) {
            var modelType = viewModel.GetType();
            var modelName = modelType.Name;

            var viewName = modelName.EndsWith("Model")
                               ? modelName.Remove(modelName.Length - 5)
                               : modelName + "View";

            var viewType = modelType.Assembly.GetTypes().Where(t => t.Name == viewName).SingleOrDefault();
            if (viewType == null)
                throw new ArgumentException("Could not find View for '{0}'".Fmt(modelName));

            if (!typeof (UIElement).IsAssignableFrom(viewType))
                throw new ArgumentException("Type '{0}' is not a UIElement".Fmt(viewType.Name));

            var view = (UIElement) _kernel.Get(viewType);

            InitializeView(view);

            return view;
        }


        private void InitializeView(UIElement view) {
            var type = view.GetType();

            var initializeMethod = type.GetMethod("InitializeComponent", BindingFlags.Public | BindingFlags.Instance);

            if (initializeMethod == null)
                return;

            initializeMethod.Invoke(view, null);
        }
    }
}