using MySql.Data.MySqlClient;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.DataProvider
{
    public class BasketDBConnection
    {
        private readonly MySqlConnection _connection;
        public BasketDBConnection()
        {
            _connection = SqlDBConnection.GetDBConnection();
        }
        public void AddOrder(OrderModel order, ObservableCollection<BasketItemModel> orderItems)
        {
            var connection = _connection;
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO orders (customer_name, customer_email, customer_phone, order_date) 
                                        VALUES (@CustomerName, @CustomerEmail, @CustomerPhone, @OrderDate);
                                        SELECT LAST_INSERT_ID();";
            command.Parameters.AddWithValue("@CustomerName", order.CustomerName);
            command.Parameters.AddWithValue("@CustomerEmail", order.CustomerEmail);
            command.Parameters.AddWithValue("@CustomerPhone", order.CustomerPhone);
            command.Parameters.AddWithValue("@OrderDate", order.OrderDate);

            int orderId = Convert.ToInt32(command.ExecuteScalar());

            foreach (var orderItem in orderItems)
            {
                command.CommandText = @"INSERT INTO order_items (order_id, item_id, item_name, item_price, item_quantity)
                                            VALUES (@OrderId, @ItemId, @ItemName, @ItemPrice, @ItemQuantity)";
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.Parameters.AddWithValue("@ItemId", orderItem.ItemId);
                command.Parameters.AddWithValue("@ItemName", orderItem.ItemName);
                command.Parameters.AddWithValue("@ItemPrice", orderItem.ItemPrice);
                command.Parameters.AddWithValue("@ItemQuantity", orderItem.ItemQuantity);

                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }
    }
}
