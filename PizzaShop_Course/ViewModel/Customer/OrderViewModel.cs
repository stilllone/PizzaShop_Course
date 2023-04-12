using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel.Customer
{
    public class OrderViewModel : PropertyBase
    {
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
            if (UserViewModel.CurrentUser.Login == null && UserViewModel.CurrentUser.Password == null)
            {
                SendMessage("You need to authorize");
            }
            else if (BasketNullable() != true && UserViewModel.CurrentUser != null)
            {
                try
                {
                    order = new OrderModel()
                    {
                        CustomerName = $"{UserViewModel.CurrentUser.FirstName}  {UserViewModel.CurrentUser}",
                        CustomerEmail = UserViewModel.CurrentUser.Email,
                        CustomerPhone = UserViewModel.CurrentUser.Number,
                        CustomerId = UserViewModel.CurrentUser.Id,
                        TotalPrice = BasketViewModel.OrderItems.Sum(item => item.ItemPrice),
                        City = this.city,
                        Street = this.street,
                        Entrance = (int)this.entrance,
                        Flat = (int)this.flat,
                        Floor = (int)this.floor,
                        HouseNumber = this.house
                    };
                    if(order == null)
                    {
                        throw new Exception();
                    }
                    BasketDBConnection dataAccessLayer = new BasketDBConnection();
                    dataAccessLayer.AddOrder(order, BasketViewModel.OrderItems);
                    MessageBox.Show("Order added successfully!");
                    ClearItems();
                }
                catch (MySqlException ex)
                {
                    Debug.WriteLine($"Exeption: {ex}");
                    SendMessage("Something went wrong with our DB");
                }
                catch (Exception ex)
                {
                    SendMessage("Something wrong with your order or data");
                }
            }
            else
                SendMessage("You have no items in basket");
        }
        private bool BasketNullable()
        {
            if (BasketViewModel.OrderItems.Count != 0)
                return false;
            else
                return true;
        }
        private void ClearItems()
        {
            order = new OrderModel();
            BasketViewModel.OrderItems.Clear();
            city = null;
            street = null;
            entrance = null;
            house = null;
            flat = null;
            floor = null;
        }
        public void SendMessage(string message)
        {
            EventAggregator.Instance.NotificationEvent.Publish(message);
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
        private int? entrance;
        public int? Entrance
        {
            get => entrance;
            set
            {
                entrance = value;
                OnPropertyChanged(nameof(Entrance));
            }
        }
        private string house = null;
        public string House
        {
            get => house;
            set
            {
                house = value;
                OnPropertyChanged(nameof(House));
            }
        }
        private int? flat;
        public int? Flat
        {
            get => flat;
            set
            {
                flat = value;
                OnPropertyChanged(nameof(Entrance));
            }
        }
        private int? floor;
        public int? Floor
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
