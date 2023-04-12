using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class PizzasViewModel : PropertyBase
    {
        public PizzasViewModel()
        {
            PizzasDBConnection pizzasDBConnection = new PizzasDBConnection();
            Pizzas = pizzasDBConnection.GetPizzas();
        } 

        private ObservableCollection<PizzasModel> pizzas;
        public ObservableCollection<PizzasModel> Pizzas
        {
            get => pizzas;
            set
            {
                pizzas = value;
                OnPropertyChanged(nameof(Pizzas));
            }
        }
        public ICommand AddPizzasToBasket
        {
            get => new RelayCommand(AddPizza);
            set
            {
                AddPizzasToBasket?.Execute(value);
            }
        }
        private void AddPizza(object pizza)
        {
            var selectedItem = pizza as PizzasModel;
            if (selectedItem != null)
            {
                var newItem = new BasketItemModel()
                {
                    ItemId = selectedItem.Id,
                    ItemName = selectedItem.Name,
                    ItemPrice = selectedItem.Price,
                    ItemSize = selectedItem.Size,
                    ItemPhoto = selectedItem.Photo,
                    ItemCount = 1
                };
                var existingItem = BasketViewModel.OrderItems.FirstOrDefault(item =>
                                item.ItemName == newItem.ItemName && item.ItemSize == newItem.ItemSize);
                if (existingItem != null)
                {
                    existingItem.ItemCount++;
                }
                else
                {
                    BasketViewModel.OrderItems.Add(newItem);
                }
                OnGlobalPropertyChanged(nameof(BasketViewModel.OrderItems));
            }
        }
    }
}
