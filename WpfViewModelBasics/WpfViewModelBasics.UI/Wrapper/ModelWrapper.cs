using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.ViewModel;

namespace WpfViewModelBasics.UI.Wrapper
{
    public class ModelWrapper<T> : Observable, IRevertibleChangeTracking
    {
        private readonly Dictionary<string, object> _originalValues;
        private readonly List<IRevertibleChangeTracking> _trackingObjects;

        private const string IsChangedPostFix = "IsChanged";

        public ModelWrapper(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            this.Model = model;
            this._originalValues = new Dictionary<string, object>();
            this._trackingObjects = new List<IRevertibleChangeTracking>();
        }

        public T Model { get; set; }

        public bool IsChanged
        {
            get
            {
                return this._originalValues.Any() || this._trackingObjects.Any(a => a.IsChanged);
            }
        }

        public void RejectChanges()
        {
            foreach (var originalValue in this._originalValues)
            {
                typeof(T).GetProperty(originalValue.Key).SetValue(Model, originalValue.Value);
            }
            this._originalValues.Clear();

            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.RejectChanges();
            }
            OnPropertyChanged("");
        }

        public void AcceptChanges()
        {
            this._originalValues.Clear();
            foreach (var trackingObject in _trackingObjects)
            {
                trackingObject.AcceptChanges();
            }
            OnPropertyChanged("");
        }

        protected void SetValue<TValue>(TValue newValue, [CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Model);
            if (!Equals(currentValue, newValue))
            {
                UpdateOriginalValue(currentValue, newValue,propertyName);
                propertyInfo.SetValue(Model, newValue);
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName + IsChangedPostFix);
            }
        }

        protected void UpdateOriginalValue(object currentValue, object newValue, string propertyName)
        {
            if (!this._originalValues.ContainsKey(propertyName))
            {
                this._originalValues.Add(propertyName, currentValue);
                OnPropertyChanged(IsChangedPostFix);
            }
            else if (Equals(_originalValues[propertyName], newValue))
            {
                this._originalValues.Remove(propertyName);
                OnPropertyChanged(IsChangedPostFix);
            }
        }

        protected TValue GetValue<TValue>([CallerMemberName] string propertyName = null)
        {
            var propertyInfo = Model.GetType().GetProperty(propertyName);
            var currentValue = propertyInfo.GetValue(Model);
            return (TValue)currentValue;
        }

        protected TValue GetOriginalValue<TValue>(string propertyName)
        {
            var result = this._originalValues.ContainsKey(propertyName)
                ? (TValue) this._originalValues[propertyName]
                : GetValue<TValue>(propertyName);
            return result;
        }

        protected bool GetIsChanged(string propertyName)
        {
            var result = this._originalValues.ContainsKey(propertyName);
            return result;
        }

        protected void RegisterCollection<TWrapper, TModel>(ChangeTrackingCollection<TWrapper> wrapperCollection, List<TModel> modelCollection)
         where TWrapper : ModelWrapper<TModel>
        {
            wrapperCollection.CollectionChanged += (s, e) =>
            {
                modelCollection.Clear();
                modelCollection.AddRange(wrapperCollection.Select(a => a.Model));
            };
            RegisterTrackingObject(wrapperCollection);
        }

        protected void RegisterComplexProperties<TModel>(ModelWrapper<TModel> wrapper)
        {
            RegisterTrackingObject(wrapper);
        }

        private void RegisterTrackingObject<TTrackingObject>(TTrackingObject trackingObject)
            where TTrackingObject : IRevertibleChangeTracking, INotifyPropertyChanged
        {
            if (!this._trackingObjects.Contains(trackingObject))
            {
                this._trackingObjects.Add(trackingObject);
                trackingObject.PropertyChanged += TrackingObjectPropertyChanged;
            }
        }

        private void TrackingObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsChanged))
            {
                OnPropertyChanged(nameof(IsChanged));
            }
        }
    }
}
