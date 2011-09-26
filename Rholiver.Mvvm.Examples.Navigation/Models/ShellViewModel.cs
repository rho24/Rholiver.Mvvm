using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Navigation;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class ShellViewModel : ViewModelBase
    {
        public IElementManager ExamplesPage { get; set; }

        public ShellViewModel(IElementManager examplesPage) {
            ExamplesPage = examplesPage;
            ExamplesPage.Initialize(() => NotifyPropertyChange(() => ExamplesPage));
        }

        public void DialogExamples() {
            ExamplesPage.NavigateTo<DialogExamplesViewModel>();
        }
    }
}