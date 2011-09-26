using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class SimpleDialogModel : DialogModelBase
    {
        public void Ok() {
            Return();
        }
    }
}