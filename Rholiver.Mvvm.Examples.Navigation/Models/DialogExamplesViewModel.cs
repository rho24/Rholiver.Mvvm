using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Navigation;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class DialogExamplesViewModel:ViewModelBase
    {
        private readonly IDialogManager _dialogManager;

        private string _dialogReturnResult;

        public DialogExamplesViewModel(IDialogManager dialogManager) {
            _dialogManager = dialogManager;
        }

        public string DialogReturnResult {
            get { return _dialogReturnResult; }
            set {
                _dialogReturnResult = value;
                NotifyPropertyChange(() => DialogReturnResult);
            }
        }

        public void OpenSimpleDialog() {
            _dialogManager.OpenDialog<SimpleDialogModel>(() => { });
        }

        public void OpenReturningDialog() {
            _dialogManager.OpenDialog<ReturningDialogModel, string>(message => DialogReturnResult = message);
        }
    }
}