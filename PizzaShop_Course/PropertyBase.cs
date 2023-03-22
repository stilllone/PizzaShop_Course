using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course
{
    public class PropertyBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));


        private static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { }; //update static property
        protected static void OnGlobalPropertyChanged(string propertyName)
        {
            GlobalPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
