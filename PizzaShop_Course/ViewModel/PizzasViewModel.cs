using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            pizzas = pizzasDBConnection.GetPizzas();
            AddPizzasToBasket = new RelayCommand(AddPizza);
            DeletePizzaFromBasket = new RelayCommand(DeletePizza);
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
        public ICommand AddPizzasToBasket { get; }
        public ICommand DeletePizzaFromBasket { get; }
        private void AddPizza(object pizza)
        {
            BasketViewModel.OrderItems.Add(item: (BasketItemModel)pizza );
        }
        private void DeletePizza(object pizza)
        {
            BasketViewModel.OrderItems.Remove(item: (BasketItemModel)pizza);
        }
    }
}
