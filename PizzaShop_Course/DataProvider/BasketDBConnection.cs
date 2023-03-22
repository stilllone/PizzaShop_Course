using MySql.Data.MySqlClient;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using System.Xml.Linq;

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
            int orderId = 0;
            MySqlCommand command = connection.CreateCommand();
            command.CommandText = @"INSERT INTO orders (customer_name, customer_phone, city, street, house, entrance, flat, customer_id, order_price) 
                                        VALUES (@CustomerName, @CustomerPhone, @City, @Street, @House, @Entrance, @Flat, @CustomerId, @OrderPrice);";
            command.Parameters.AddWithValue("@CustomerName", order.CustomerName);
            command.Parameters.AddWithValue("@CustomerPhone", order.CustomerPhone);
            command.Parameters.AddWithValue("@CustomerId", order.CustomerId);
            command.Parameters.AddWithValue("@City", order.City);
            command.Parameters.AddWithValue("@Street", order.Street);
            command.Parameters.AddWithValue("@House", order.HouseNumber);
            command.Parameters.AddWithValue("@Entrance", order.Entrance);
            command.Parameters.AddWithValue("@Flat", order.Flat);
            command.Parameters.AddWithValue("@OrderPrice", order.TotalPrice);
            command.CommandText = "SELECT MAX(id) FROM orders;";
            MySqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderId = reader.GetInt32("MAX(id)");
            }
            reader.Close();

            foreach (var orderItem in orderItems)
            {
                command.CommandText = @"INSERT INTO order_items (order_id,item_id, item_name, item_price, item_size)
                                            VALUES (@OrderId, @ItemId, @ItemName, @ItemPrice, @ItemSize )";
                command.Parameters.AddWithValue("@ItemId", orderItem.ItemId);
                command.Parameters.AddWithValue("@ItemName", orderItem.ItemName);
                command.Parameters.AddWithValue("@ItemPrice", orderItem.ItemPrice);
                command.Parameters.AddWithValue("@ItemSize", orderItem.ItemSize);
                command.Parameters.AddWithValue("@OrderId", orderId);
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
            connection.Close();
        }
    }
}
