using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel.Customer
{
    public class OrderViewModel : PropertyBase
    {
        public OrderViewModel()
        {
        }
        public ICommand AddOrderCommand { get => new RelayCommand(AddOrder); }
        private OrderModel order;
        public OrderModel Order
        {
            get => order;
            set
            {
                order = value;
                OnPropertyChanged(nameof(Order));
            }
        }
        private void AddOrder(object parameter)
        {
            if (BasketNullable() != false)
                try
                {
                    order = new OrderModel()
                    {
                        CustomerName = $"{UserViewModel.CurrentUser.FirstName}  {UserViewModel.CurrentUser}",
                        CustomerEmail = UserViewModel.CurrentUser.Email,
                        CustomerPhone = UserViewModel.CurrentUser.Number,

                    };
                    BasketDBConnection dataAccessLayer = new BasketDBConnection();
                    dataAccessLayer.AddOrder(order, BasketViewModel.OrderItems);
                    MessageBox.Show("Order added successfully!");
                    order = new OrderModel();
                    BasketViewModel.OrderItems = new ObservableCollection<BasketItemModel>();
                    OnPropertyChanged(nameof(Order));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Something went wrong");
                }
            else
                MessageBox.Show("You have no items in basket");
        }
        private bool BasketNullable()
        {
            if (BasketViewModel.OrderItems.Count != 0)
                return true;
            else
                return false;
        }
        #region prop
        public Towns[] Cities
        {
            get { return (Towns[])Enum.GetValues(typeof(Towns)); }
        }
        private string city;
        public string City
        {
            get => city;
            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnPropertyChanged(nameof(Street));
            }
        }
        private int entrance;
        public int Entrance
        {
            get => entrance;
            set
            {
                entrance = value;
                OnPropertyChanged(nameof(Entrance));
            }
        }
        private int flat;
        public int Flat
        {
            get => flat;
            set
            {
                flat = value;
                OnPropertyChanged(nameof(Entrance));
            }
        }
        private int floor;
        public int Floor
        {
            get => floor;
            set
            {
                floor = value;
                OnPropertyChanged(nameof(Floor));
            }
        }
        #endregion
    }
}
