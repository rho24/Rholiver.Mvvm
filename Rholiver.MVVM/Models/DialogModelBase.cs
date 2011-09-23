using System;

namespace Rholiver.Mvvm.Models
{
    public abstract class DialogModelBase : ViewModelBase, IDialogModel
    {
        public Action OnReturn { get; set; }
        public Action OnCancel { get; set; }

        public virtual void Return() {
            OnReturn();
        }

        public virtual void Cancel() {
            OnCancel();
        }
    }

    public abstract class DialogModelBase<T> : ViewModelBase, IDialogModel<T>
    {
        public Action<T> OnReturn { get; set; }
        public Action OnCancel { get; set; }

        public virtual void Return(T value) {
            OnReturn(value);
        }

        public virtual void Cancel() {
            OnCancel();
        }
    }
}