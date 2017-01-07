namespace WpfViewModelBasics.UI.ViewModel.Base
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class ViewModelBase : Observable
    {
        /// <summary>
        ///     Stores the values for each property in the current object.
        /// </summary>
        private readonly Dictionary<string, object> _backingFields = new Dictionary<string, object>();

        /// <summary>
        ///     Gets the current value of a property.
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="propertyName">The property name.</param>
        /// <returns>The current value.</returns>
        protected T GetValue<T>([CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            object value;
            if (_backingFields.TryGetValue(propertyName, out value))
            {
                return (T) value;
            }
            return default(T);
        }

        /// <summary>
        ///     Sets the value of a property
        /// </summary>
        /// <typeparam name="T">The property type.</typeparam>
        /// <param name="newValue">the new value of the property</param>
        /// <param name="propertyName">The property name.</param>
        protected void SetValue<T>(T newValue, [CallerMemberName] string propertyName = null)
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException("propertyName");
            }

            if (EqualityComparer<T>.Default.Equals(newValue, GetValue<T>(propertyName)))
            {
                return;
            }
            _backingFields[propertyName] = newValue;
            OnPropertyChanged(propertyName);
        }
    }
}