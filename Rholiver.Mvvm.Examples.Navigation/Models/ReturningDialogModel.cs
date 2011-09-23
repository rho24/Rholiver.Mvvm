using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class ReturningDialogModel : DialogModelBase<string>
    {
        public void Ok() {
            Return(Message);
        }

        private string _message;

        public string Message {
            get { return _message; }
            set {
                _message = value;
                NotifyPropertyChange(() => Message);
            }
        }
    }
}