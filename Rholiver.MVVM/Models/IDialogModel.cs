using System;

namespace Rholiver.Mvvm.Models
{
    public interface IDialogModel : IViewModel
    {
        Action OnReturn { get; set; }
        Action OnCancel { get; set; }
    }

    public interface IDialogModel<T> : IViewModel
    {
        Action<T> OnReturn { get; set; }
        Action OnCancel { get; set; }
    }
}