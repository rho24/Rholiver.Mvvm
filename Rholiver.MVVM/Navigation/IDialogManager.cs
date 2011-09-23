using System;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Navigation
{
    public interface IDialogManager
    {
        void OpenDialog<T>(Action onReturn) where T : IDialogModel;
        void OpenDialog<T, TReturn>(Action<TReturn> onReturn) where T : IDialogModel<TReturn>;
    }
}