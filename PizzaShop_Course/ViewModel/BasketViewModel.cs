using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class BasketViewModel : PropertyBase
    {
        public BasketViewModel()
        {
            AddOrderCommand = new RelayCommand(AddOrder);
        }

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
        public static ObservableCollection<BasketItemModel> OrderItems = new ObservableCollection<BasketItemModel>();

        public ICommand AddOrderCommand { get; set; }

        private void AddOrder(object parameter)
        {
            order = new OrderModel() {
                CustomerName = $"{UserViewModel.CurrentUser.FirstName}  {UserViewModel.CurrentUser}",
                CustomerEmail = UserViewModel.CurrentUser.Email,
                CustomerPhone = UserViewModel.CurrentUser.Number
            };
            BasketDBConnection dataAccessLayer = new BasketDBConnection();
            dataAccessLayer.AddOrder(order, OrderItems);
            MessageBox.Show("Order added successfully!");
            order = new OrderModel();
            OrderItems = new ObservableCollection<BasketItemModel>();
            OnPropertyChanged(nameof(Order));
        }
        private static string totalprice = OrderItems.Sum(item => item.ItemPrice).ToString();
        public static string TotalPrice
        {
            get => totalprice;
            set
            {
                if (totalprice != value)
                    totalprice = value;
                OnGlobalPropertyChanged(nameof(TotalPrice));
            }
        }

        //public ICommand SaveOrderCommand => new RelayCommand(SaveOrder);

        //private void SaveOrder(object parameter)
        //{
        //    var connectionString = "your connection string here";
        //    using (var connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        using (var transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                // Insert the order record
        //                var orderCommand = new MySqlCommand("INSERT INTO orders (customer_name, customer_email, customer_phone) VALUES (@customerName, @customerEmail, @customerPhone); SELECT LAST_INSERT_ID();", connection, transaction);
        //                orderCommand.Parameters.AddWithValue("@customerName", CustomerName);
        //                orderCommand.Parameters.AddWithValue("@customerEmail", CustomerEmail);
        //                var orderId = (int)orderCommand.ExecuteScalar();

        //                // Insert the order item records
        //                foreach (var orderItem in OrderItems)
        //                {
        //                    var orderItemCommand = new MySqlCommand("INSERT INTO order_items (order_id, item_id, item_name, item_price, item_quantity) VALUES (@orderId, @itemId, @itemName, @itemPrice, @itemQuantity);", connection, transaction);
        //                    orderItemCommand.Parameters.AddWithValue("@orderId", orderId);
        //                    orderItemCommand.Parameters.AddWithValue("@itemId", orderItem.ItemId);
        //                    orderItemCommand.Parameters.AddWithValue("@itemName", orderItem.ItemName);
        //                    orderItemCommand.Parameters.AddWithValue("@itemPrice", orderItem.ItemPrice);
        //                    orderItemCommand.Parameters.AddWithValue("@itemQuantity", orderItem.ItemQuantity);
        //                    orderItemCommand.ExecuteNonQuery();
        //                }

        //                // Commit the transaction
        //                transaction.Commit();
        //            }
        //            catch (Exception ex)
        //            {
        //                // Roll back the transaction on error
        //                transaction.Rollback();
        //                MessageBox.Show($"Error saving order: {ex.Message}");
        //            }
        //        }
        //    }
        //}


        //private static ObservableCollection<BasketModel> items;
        //public static ObservableCollection<BasketModel> Items
        //{
        //    get { return items; }
        //    set
        //    {
        //        items = value;
        //        OnGlobalPropertyChanged(nameof(Items));
        //    }
        //}
        //private static double totalprice = Items.Sum(item => item.Pizzas.Price) + Items.Sum(item => item.Drinks.Price);
        //public static double TotalPrice
        //{
        //    get => totalprice;
        //    set
        //    {
        //        if (totalprice != value)
        //            totalprice = value;
        //        OnGlobalPropertyChanged(nameof(TotalPrice));
        //    }
        //}



        //public double TotalPrice => Items.Sum(item => item.Price);
        //private static double totalprice;
        //public static double TotalPrice
        //{
        //    get => totalprice;
        //    set
        //    {
        //        if (totalprice != value)
        //            totalprice = value;
        //        OnGlobalPropertyChanged(nameof(TotalPrice));
        //    }
        //}    


        //public void AddItem(object parameter)//abstract item
        //{
        //    if (parameter is ObservableCollection<PizzasModel> item)
        //    {
        //        Items.Add(new BasketModel() { Pizzas = item});
        //    }
        //}
    }
}
