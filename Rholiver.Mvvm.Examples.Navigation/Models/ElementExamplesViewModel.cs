using System;
using Rholiver.Mvvm.Models;
using Rholiver.Mvvm.Navigation;

namespace Rholiver.Mvvm.Examples.Navigation.Models
{
    public class ElementExamplesViewModel : ViewModelBase
    {
        private bool _singleElementIsChecked;
        
        public bool SingleElementIsChecked {
            get { return _singleElementIsChecked; }
            set {
                _singleElementIsChecked = value;
                ToggleSingleElement(value);
            }
        }

        public IElementManager SingleElement { get; set; }
        public IMultiElementManager MultipleElements { get; set; }

        public ElementExamplesViewModel(IElementManager singleElement, IMultiElementManager multipleElement) {
            SingleElement = singleElement;
            SingleElement.Initialize(() => NotifyPropertyChange(() => SingleElement));

            MultipleElements = multipleElement;
        }

        private void ToggleSingleElement(bool shouldShow) {
            if (shouldShow)
                SingleElement.NavigateTo<SingleElementViewModel>();
            else {
                SingleElement.Clear();
            }
        }

        public void AddMultipleElement() {
            MultipleElements.Add<SingleElementViewModel>();
        }

        public void RemoveMultipleElement() {
            if(MultipleElements.ElementValues.Count > 0)
                MultipleElements.ElementValues.RemoveAt(0);
        }
    }
}