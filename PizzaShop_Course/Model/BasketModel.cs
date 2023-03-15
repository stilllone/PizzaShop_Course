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
        private PizzasModel pizzas;
        public PizzasModel Pizzas 
        {
            get => pizzas;
            set
            {
                pizzas = value;
                OnPropertyChanged(nameof(Pizzas));
            }
        }

        private DrinksModel drinks;
        public DrinksModel Drinks 
        {
            get => drinks;
            set
            {
                drinks = value;
                OnPropertyChanged(nameof(Drinks));
            }
        }
    }
}
