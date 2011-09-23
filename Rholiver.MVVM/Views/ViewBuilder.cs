using System;
using Ninject;
using Rholiver.Mvvm.Infrastructure;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Views
{
    public class ViewBuilder : IViewBuilder
    {
        private readonly IKernel _kernel;
        private readonly IViewLocator _viewLocator;
        private readonly IViewWeaver _viewWeaver;

        public ViewBuilder(IKernel kernel, IViewLocator viewLocator, IViewWeaver viewWeaver) {
            _kernel = kernel;
            _viewLocator = viewLocator;
            _viewWeaver = viewWeaver;
        }

        public ModelAndView<T> Build<T>() where T : IViewModel {
            var model = _kernel.Get<T>();

            if (model == null)
                throw new ArgumentException("Cannot create model '{0}'".Fmt(typeof (T).Name));

            return Build(model);
        }

        public ModelAndView<T> Build<T>(T viewModel) where T : IViewModel {
            var view = _viewLocator.GetView(viewModel);

            _viewWeaver.Weave(view, viewModel);

            return new ModelAndView<T> {Model = viewModel, View = view};
        }
    }
}