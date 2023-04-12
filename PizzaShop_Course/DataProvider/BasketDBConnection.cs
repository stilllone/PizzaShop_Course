using MySql.Data.MySqlClient;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Diagnostics;
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
            MySqlTransaction transaction = connection.BeginTransaction();

            try
            {
                MySqlCommand command = connection.CreateCommand();
                command.Transaction = transaction;

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
                command.ExecuteNonQuery();

                command.CommandText = "SELECT LAST_INSERT_ID();";
                int orderId = Convert.ToInt32(command.ExecuteScalar());

                foreach (var orderItem in orderItems)
                {
                    command.CommandText = @"INSERT INTO order_items (order_id,item_id, item_name, item_price, item_size, item_count)
                                        VALUES (@OrderId, @ItemId, @ItemName, @ItemPrice, @ItemSize, @ItemCount )";
                    command.Parameters.AddWithValue("@OrderId", orderId);
                    command.Parameters.AddWithValue("@ItemId", orderItem.ItemId);
                    command.Parameters.AddWithValue("@ItemName", orderItem.ItemName);
                    command.Parameters.AddWithValue("@ItemPrice", orderItem.ItemPrice);
                    command.Parameters.AddWithValue("@ItemSize", orderItem.ItemSize);
                    command.Parameters.AddWithValue("@ItemCount", orderItem.ItemCount);
                    command.ExecuteNonQuery();
                    command.Parameters.Clear();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Debug.WriteLine(ex);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
