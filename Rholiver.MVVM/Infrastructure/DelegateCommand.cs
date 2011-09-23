using System;
using System.Windows.Input;

namespace Rholiver.Mvvm.Infrastructure
{
    internal class DelegateCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Func<object, bool> _canExecute;
        private bool? _canExecuteCached;

        public DelegateCommand(Action<object> action, Func<object, bool> canExecute) {
            _action = action;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) {
            var canExecuteNow = _canExecute(parameter);
            if (_canExecuteCached.HasValue && _canExecuteCached.Value != canExecuteNow)
                OnCanExecuteChanged(null);

            _canExecuteCached = canExecuteNow;
            return canExecuteNow;
        }

        public void Execute(object parameter) {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void OnCanExecuteChanged(EventArgs e) {
            EventHandler handler = CanExecuteChanged;
            if (handler != null) handler(this, e);
        }
    }
}