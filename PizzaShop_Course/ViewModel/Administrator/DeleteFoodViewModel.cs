using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel.Administrator
{
    public class DeleteFoodViewModel : PropertyBase
    {
        private PizzasDBConnection pizzasDB;
        private DrinksDBConnection drinksDB;
        public DeleteFoodViewModel()
        {
            this.pizzasDB = new PizzasDBConnection();
            this.drinksDB = new DrinksDBConnection();
            PizzasDBConnection pizzasDBConnection = new PizzasDBConnection();
            Pizzas = pizzasDBConnection.GetPizzas();
            DrinksDBConnection drinksModel = new DrinksDBConnection();
            Drinks = drinksModel.GetDrinks();
        }

        private ObservableCollection<DrinksModel> drinks;
        public ObservableCollection<DrinksModel> Drinks
        {
            get => drinks;
            set
            {
                drinks = value;
                OnPropertyChanged(nameof(Drinks));
            }
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
        public IEnumerable<FoodItems> CBItems
        {
            get
            {
                return Enum.GetValues(typeof(FoodItems)).Cast<FoodItems>();
            }
        }

        private FoodItems currentItem;
        public FoodItems CurrentItem
        {
            get => currentItem;
            set
            {
                if (currentItem != value)
                    currentItem = value;
                OnPropertyChanged(nameof(CurrentItem));
                Debug.WriteLine(CurrentItem);
            }
        }
        public ICommand Delete { get => new RelayCommand(DeleteFoodFromDb); }

        private void DeleteFoodFromDb(object obj)
        {
            try
            {
                if (CurrentItem == FoodItems.Pizza)
                {
                    if (obj is PizzasModel pizza && pizza.Id != null)
                    {
                        try
                        {
                            pizzasDB.DeletePizza(pizza.Id);
                            EventAggregator.Instance.NotificationEvent.Publish("Pizza was deleted");
                        }
                        catch (MySqlException ex)
                        {
                            EventAggregator.Instance.NotificationEvent.Publish("Failed delete pizza");
                        }
                    }
                }
                else if (CurrentItem == FoodItems.Drink)
                {
                    if (obj is DrinksModel drink && drink.Id != null)
                    {
                        try
                        {
                            drinksDB.DeleteDrinks(drink.Id);
                            EventAggregator.Instance.NotificationEvent.Publish("Drink was deleted");
                        }
                        catch (MySqlException ex)
                        {
                            EventAggregator.Instance.NotificationEvent.Publish("Failed delete pizza");
                        }
                    }
                }
            }
            catch (MySqlException ex)
            {
                EventAggregator.Instance.NotificationEvent.Publish("mySQL exception");
            }
            finally
            {
                if (CurrentItem == FoodItems.Drink)
                {
                    RefreshDrink();
                }
                else if (CurrentItem == FoodItems.Pizza)
                {
                    RefreshPizza();
                }
            }
        }
        private void RefreshPizza()
        {
            Pizzas = new ObservableCollection<PizzasModel>(pizzasDB.GetPizzas());
            OnPropertyChanged(nameof(Pizzas));
        }
        private void RefreshDrink()
        {
            Drinks = new ObservableCollection<DrinksModel>(drinksDB.GetDrinks());
            OnPropertyChanged(nameof(Drinks));
        }
    }

}
