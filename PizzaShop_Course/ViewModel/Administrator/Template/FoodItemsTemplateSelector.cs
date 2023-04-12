using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace PizzaShop_Course.ViewModel.Administrator.Template
{
    public class FoodItemsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DrinkTemplate { get; set; }
        public DataTemplate PizzaTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is DrinksModel)
                return DrinkTemplate;
            else if (item is PizzasModel)
                return PizzaTemplate;
            else
                return null;
        }
    }
}
