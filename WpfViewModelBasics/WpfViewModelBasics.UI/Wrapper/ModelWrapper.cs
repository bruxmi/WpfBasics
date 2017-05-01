using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WpfViewModelBasics.UI.ViewModel;

namespace WpfViewModelBasics.UI.Wrapper
{
    public class ModelWrapper<T> : NotifyDataErrorInfoBase, IValidatableTrackingObject, IValidatableObject
    {
        private readonly Dictionary<string, object> _originalValues;
        private readonly List<IValidatableTrackingObject> _trackingObjects;

        private const string IsChangedPostFix = "IsChanged";

        public ModelWrapper(T model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }
            this.Model = model;
            this._originalValues = new Dictionary<string, object>();
            this._trackingObjects = new List<IValidatableTrackingObject>();
            InitializeComplexProperties(model);
            InitializeCollectionProperties(model);
            this.Validate();
        }

        protected virtual void InitializeComplexProperties(T model)
        {
        }

        protected virtual void InitializeCollectionProperties(T model)
        {
        }

        public T Model { get; set; }

        public bool IsChanged => this._originalValues.Any() || this._trackingObjects.Any(a => a.IsChanged);

        public bool IsValid => !this.HasErrors && _trackingObjects.All(t => t.IsValid);

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
            this.Validate();
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
                UpdateOriginalValue(currentValue, newValue, propertyName);
                propertyInfo.SetValue(Model, newValue);
                Validate();
                OnPropertyChanged(propertyName);
                OnPropertyChanged(propertyName + IsChangedPostFix);
            }
        }

        private void Validate()
        {
            ClearErrors();
            var results = new List<ValidationResult>();
            var customErrors = new List<CustomErrorResult>();
            var context = new ValidationContext(this);
            Validator.TryValidateObject(this, context, results, true);
            if (results.Any())
            {
                foreach (var result in results)
                {
                    var error = (CustomErrorResult)result;
                    customErrors.Add(error);
                }
                var propertyNames = results.SelectMany(a => a.MemberNames).Distinct().ToList();
                foreach (var propertyName in propertyNames)
                {
                    this.Errors[propertyName] = customErrors.Where(a => a.MemberNames.Contains(propertyName)).Select(a => a).Distinct().ToList();
                    OnErrorsChanged(propertyName);
                }
            }
            OnPropertyChanged(nameof(this.IsValid));
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
                ? (TValue)this._originalValues[propertyName]
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
                Validate();
            };
            RegisterTrackingObject(wrapperCollection);
        }

        protected void RegisterComplexProperties<TModel>(ModelWrapper<TModel> wrapper)
        {
            RegisterTrackingObject(wrapper);
        }

        private void RegisterTrackingObject(IValidatableTrackingObject trackingObject)
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
            else if (e.PropertyName == nameof(IsValid))
            {
                OnPropertyChanged(nameof(IsValid));
            }
        }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

    public class CustomErrorResult : ValidationResult
    {
        public enum ErrorLevel
        {
            Error,
            Warning
        }

        public ErrorLevel Level { get; }

        public CustomErrorResult(string errorMessage, string[] propertyNames, ErrorLevel level) : base(errorMessage, propertyNames)
        {
            this.Level = level;
        }
    }

}
