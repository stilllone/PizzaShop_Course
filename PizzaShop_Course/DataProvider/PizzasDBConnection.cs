using MySql.Data.MySqlClient;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.DataProvider
{
    public class PizzasDBConnection
    {
        private readonly MySqlConnection _connection;

        public PizzasDBConnection()
        {
            _connection = SqlDBConnection.GetDBConnection();
        }

        public ObservableCollection<PizzasModel> GetPizzas()
        {
            ObservableCollection<PizzasModel> pizzas = new ObservableCollection<PizzasModel>();
            using (var connection = _connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM pizzas", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] photoBytes = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("photo")))
                    {
                        photoBytes = (byte[])reader["photo"];
                    }
                    pizzas.Add(new PizzasModel()
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("food_name"),
                        Ingredients = reader.GetString("ingridients"),
                        Mass = reader.GetInt32("mass"),
                        Photo = photoBytes,
                        Price = (double)reader.GetDouble("price")
                    });
                }
                reader.Close();
            }
            return pizzas;
        }
        public void CreatePizza(PizzasModel pizza)
        {
            var connection = _connection;
            connection.Open();
            string query = "INSERT INTO pizzas (food_name, ingridients, price, photo, mass) " +
               "VALUES (@name, @ingridients, @price, @photo, @mass)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", pizza.Name);
            command.Parameters.AddWithValue("@ingridients", pizza.Ingredients);
            command.Parameters.AddWithValue("@size", pizza.Size);
            command.Parameters.AddWithValue("@price", Math.Round(pizza.Price,2));
            command.Parameters.AddWithValue("@photo", pizza.Photo);
            command.Parameters.AddWithValue("@mass", pizza.Mass);
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void DeletePizza(int id)
        {
            var connection = _connection;
            connection.Open();
            string query = "DELETE FROM pizzas WHERE id = @pizzaId;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@pizzaId", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                EventAggregator.Instance.NotificationEvent.Publish($"No pizza with ID {id} found in database.");
            }
            connection.Close();
        }
    }
}
