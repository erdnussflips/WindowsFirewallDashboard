using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsAdvancedFirewallApi.Utils;
using WindowsFirewallDashboard.ViewModel.Base;

namespace WindowsFirewallDashboard.ViewModel
{
    interface IGenericViewModel<TModelType> : IViewModel<TModelType>
    {
        void SetPropertyValue<TPropertyType>(string name, TPropertyType value);
    }

    class GenericViewModel<TModelType> : BaseViewModel, IGenericViewModel<TModelType>
    {
        [Browsable(false)]
        [Bindable(false)]
        public TModelType Model { get; set; }

        private IDictionary<string, IViewModel> GenericViewModels = new Dictionary<string, IViewModel>();

        protected GenericViewModel() {}

        public GenericViewModel(TModelType model) : this()
        {
            Model = model;
        }

        public IGenericViewModel<TPropertyModelType> GetGenericViewModel<TGenericViewModelType, TPropertyModelType>(string propertyName)
            where TGenericViewModelType : IGenericViewModel<TPropertyModelType>
        {
            if (GenericViewModels.ContainsKey(propertyName))
            {
                return (IGenericViewModel<TPropertyModelType>)GenericViewModels.GetValue(propertyName, null);
            }

            var genericViewModel = default(TGenericViewModelType);
            genericViewModel.Model = GetPropertyValue<TPropertyModelType>(propertyName);
            GenericViewModels.Add(propertyName, genericViewModel);
            return genericViewModel;
        }

        private TPropertyType GetPropertyValue<TPropertyType>(string name)
        {
            foreach (var propertyInfo in Model?.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                if (propertyInfo.Name.Equals(name))
                {
                    return (TPropertyType)propertyInfo.GetValue(Model);
                }
            }

            return default(TPropertyType);
        }

        public void SetPropertyValue<TPropertyType>(string name, TPropertyType value)
        {
            foreach (var propertyInfo in Model?.GetType().GetProperties(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
            {
                if (propertyInfo.Name.Equals(name))
                {
                    propertyInfo.SetValue(Model, value);
                    RaiseOnPropertyChanged(name);
                }
            }
        }
    }
}
