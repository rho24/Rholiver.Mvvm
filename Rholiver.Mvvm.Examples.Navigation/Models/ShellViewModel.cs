using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Navigation;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class ShellViewModel : ViewModelBase
    {
        private readonly IDialogManager _dialogManager;

        public ShellViewModel(IDialogManager dialogManager) {
            _dialogManager = dialogManager;
        }

        public void OpenSimpleDialog() {
            _dialogManager.OpenDialog<SimpleDialogModel>(() => { });
        }
    }
}