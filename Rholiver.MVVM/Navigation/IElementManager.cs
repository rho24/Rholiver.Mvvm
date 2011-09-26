using System;
using System.Windows;
using Rholiver.Mvvm.Models;

namespace Rholiver.Mvvm.Navigation
{
    public interface IElementManager
    {
        UIElement ElementValue { get; }
        void NavigateTo<T>() where T : IViewModel;
        void Initialize(Action onChange);
        void Clear();
    }
}