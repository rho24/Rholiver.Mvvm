using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Navigation;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IDialogManager _dialogManager;
        private string _dialogReturnResult;

        public ShellViewModel(IDialogManager dialogManager) {
            _dialogManager = dialogManager;
        }

        public void OpenSimpleDialog() {
            _dialogManager.OpenDialog<SimpleDialogModel>(() => { });
        }

        public void OpenReturningDialog() {
            _dialogManager.OpenDialog<ReturningDialogModel, string>(message => DialogReturnResult = message);
        }

        public string DialogReturnResult {
            get { return _dialogReturnResult; }
            set {
                _dialogReturnResult = value;
                NotifyPropertyChange(() => DialogReturnResult);
            }
        }
    }
}