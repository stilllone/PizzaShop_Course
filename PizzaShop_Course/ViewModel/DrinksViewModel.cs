using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.ViewModel
{
    public class DrinksViewModel : PropertyBase
    {
        private ObservableCollection<DrinksModel> drinks = new ObservableCollection<DrinksModel>();
        public ObservableCollection<DrinksModel> Drinks
        {
            get => drinks;
            set
            {
                drinks = value;
                OnPropertyChanged();
            }
        }
        public DrinksViewModel()
        {
            Drinks.Add(new DrinksModel());
        }
    }
}
