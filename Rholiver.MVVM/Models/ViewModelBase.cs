using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Rholiver.Mvvm.Models
{
    public abstract class ViewModelBase : IViewModel, INotifyPropertyChanged
    {
        protected void NotifyPropertyChange(Expression<Func<object>> propertyExpression) {
            var body = propertyExpression.Body;

            if (body is UnaryExpression && (body as UnaryExpression).NodeType == ExpressionType.Convert) body = ((UnaryExpression) body).Operand;
            var propertyName = ((MemberExpression) body).Member.Name;
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e) {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, e);
        }
    }
}