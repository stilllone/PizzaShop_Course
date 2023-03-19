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
        //public BasketViewModel()
        //{
        //    AddOrderCommand = new RelayCommand(AddOrder);
        //}
        public BasketViewModel()
        { }
        public static ObservableCollection<BasketItemModel> orderItems = new ObservableCollection<BasketItemModel>();
        public static ObservableCollection<BasketItemModel> OrderItems
        {
            get => orderItems;
            set
            {
                orderItems = value;
                OnGlobalPropertyChanged(nameof(OrderItems));
            }
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
    }
}
