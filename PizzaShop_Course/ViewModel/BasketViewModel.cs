using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class BasketViewModel : PropertyBase
    {
        public ICommand Delete
        {
            get => new RelayCommand(DeleteItem);
            set
            {
                Delete?.Execute(value);
            }
        }
        private void DeleteItem(object item)
        {
            var itemToDelete = item as BasketItemModel;
            if (itemToDelete != null)
            {
                OrderItems.Remove(itemToDelete);
            }
            Debug.WriteLine(OrderItems.Count);
        }
        public static ObservableCollection<BasketItemModel> orderItems = new ObservableCollection<BasketItemModel>();
        public static ObservableCollection<BasketItemModel> OrderItems
        {
            get { return orderItems; }
            set
            {
                orderItems = value;
                Debug.WriteLine("OrderItems changed");
                OnGlobalPropertyChanged(nameof(OrderItems));            
            }
        }
    }
}
