using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.Model
{
    public class BasketModel : PropertyBase
    {
        public ObservableCollection<PizzasModel> Items { get; set; }
        //public double TotalPrice => Items.Sum(item => item.Price);
        private double totalprice;
        public double TotalPrice
        {
            get => totalprice;
            set
            {
                totalprice = Items.Sum(item => item.Price);
                OnPropertyChanged();
            }
        }
    }
}
