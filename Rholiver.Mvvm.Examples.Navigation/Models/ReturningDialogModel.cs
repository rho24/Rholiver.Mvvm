using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class ReturningDialogModel : DialogModelBase<string>
    {
        private string _message;

        public string Message {
            get { return _message; }
            set {
                _message = value;
                NotifyPropertyChange(() => Message);
            }
        }

        public void Ok() {
            Return(Message);
        }
    }
}