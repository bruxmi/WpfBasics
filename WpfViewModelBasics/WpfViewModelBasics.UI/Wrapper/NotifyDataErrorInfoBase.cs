using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.ViewModel;

namespace WpfViewModelBasics.UI.Wrapper
{
    public class NotifyDataErrorInfoBase : Observable, INotifyDataErrorInfo
    {
        protected readonly Dictionary<string, List<string>> Errors;

        public NotifyDataErrorInfoBase()
        {
            this.Errors = new Dictionary<string, List<string>>();
        }

        public bool HasErrors => this.Errors.Any();

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        public IEnumerable GetErrors(string propertyName)
        {
            return propertyName != null && this.Errors.ContainsKey(propertyName) ? Errors[propertyName]: Enumerable.Empty<string>();
        }

        protected virtual void OnErrorsChanged(string propertyName)
        {
           ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        protected void ClearErrors()
        {
            foreach (var propertyName in Errors.Keys.ToList())
            {
                Errors.Remove(propertyName);
                OnErrorsChanged(propertyName);
            }
        }
    }
}
