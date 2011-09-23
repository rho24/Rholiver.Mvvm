using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Views
{
    public interface IViewBuilder
    {
        ModelAndView<T> Build<T>() where T : IViewModel;
        ModelAndView<T> Build<T>(T viewModel) where T : IViewModel;
    }
}