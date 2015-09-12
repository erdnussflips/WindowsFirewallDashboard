using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.ViewModel
{
	[Serializable]
	public abstract class BaseViewModel<TModel> : BaseViewModel, IViewModel<TModel> where TModel : class
	{
		private TModel model;

		[Browsable(false)]
		[Bindable(false)]
		public TModel Model
		{
			get
			{
				return model;
			}
			set
			{
				if (model != value)
				{
					// get all properties
					var properties = this.GetType().GetProperties(BindingFlags.Public);
					// all values before the model has changed
					var oldValues = properties.Select(p => p.GetValue(this, null));
					var enumerator = oldValues.GetEnumerator();

					model = value;

					// call OnPropertyChanged for all changed properties
					foreach (var property in properties)
					{
						enumerator.MoveNext();
						var oldValue = enumerator.Current;
						var newValue = property.GetValue(this, null);

						if ((oldValue == null && newValue != null)
							|| (oldValue != null && newValue == null)
							|| (!oldValue.Equals(newValue)))
						{
							OnPropertyChanged(property.Name);
						}
					}
				}
			}
		}

		protected BaseViewModel(TModel model) : base()
		{
			this.model = model;
		}

		public override int GetHashCode() => Model.GetHashCode();

		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			var other = obj as IViewModel<TModel>;

			if (other == null)
				return false;

			return Equals(other);
		}

		public bool Equals(IViewModel<TModel> other)
		{
			if (other == null)
				return false;

			if (Model == null)
				return Model == other.Model;

			return Model.Equals(other.Model);
		}
	}
}