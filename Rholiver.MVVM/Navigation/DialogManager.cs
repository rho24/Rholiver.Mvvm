using System;
using System.Windows.Controls;
using Rholiver.Mvvm.Infrastructure;
using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Views;

namespace Rholiver.Mvvm.Navigation
{
    public class DialogManager : IDialogManager
    {
        private readonly IViewBuilder _viewBuilder;

        public DialogManager(IViewBuilder viewBuilder) {
            _viewBuilder = viewBuilder;
        }

        public void OpenDialog<T>(Action onReturn) where T : IDialogModel {
            var modelAndView = _viewBuilder.Build<T>();

            var view = modelAndView.View as ChildWindow;

            if (view == null)
                throw new ArgumentException("View for '{0}' is not a ChildWindow".Fmt(typeof (T).Name));


            modelAndView.Model.OnReturn = () => {
                                              onReturn();
                                              view.Close();
                                          };

            modelAndView.Model.OnCancel = view.Close;

            view.Show();
        }

        public void OpenDialog<T, TReturn>(Action<TReturn> onReturn) where T : IDialogModel<TReturn> {
            var modelAndView = _viewBuilder.Build<T>();

            var view = modelAndView.View as ChildWindow;

            if (view == null)
                throw new ArgumentException("View for '{0}' is not a ChildWindow".Fmt(typeof (T).Name));


            modelAndView.Model.OnReturn = result => {
                                              onReturn(result);
                                              view.Close();
                                          };

            modelAndView.Model.OnCancel = view.Close;

            view.Show();
        }
    }
}