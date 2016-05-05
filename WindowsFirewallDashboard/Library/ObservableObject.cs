using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFirewallDashboard.Library
{
    public class ObservableObject : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaiseOnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void RaiseOnPropertyChangedForAllProperties()
        {
            var properties = GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var item in properties)
            {
                RaiseOnPropertyChanged(item.Name);
            }
        }
        #endregion
    }
}
