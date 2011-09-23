using System.Windows;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Views
{
    public class ModelAndView<T> where T : IViewModel
    {
        public T Model { get; set; }
        public UIElement View { get; set; }
    }
}