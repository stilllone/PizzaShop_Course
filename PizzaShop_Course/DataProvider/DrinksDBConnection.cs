using MySql.Data.MySqlClient;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.DataProvider
{
    public class DrinksDBConnection
    {
        private readonly MySqlConnection _connection;

        public DrinksDBConnection()
        {
            _connection = SqlDBConnection.GetDBConnection();
        }

        public async Task<ObservableCollection<DrinksModel>> GetDrinksAsync()
        {
            ObservableCollection<DrinksModel> drinks = new ObservableCollection<DrinksModel>();
            using (var connection = _connection)
            {
                MySqlCommand command = new MySqlCommand("SELECT * FROM drinks", connection);
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    byte[] photoBytes = null;
                    if (!reader.IsDBNull(reader.GetOrdinal("photo")))
                    {
                        photoBytes = (byte[])reader["photo"];
                    }
                    drinks.Add(new DrinksModel()
                    {
                        Id = reader.GetInt32("id"),
                        Name = reader.GetString("food_name"),
                        Photo = photoBytes,
                        Price = reader.GetDouble("price")
                    });
                }
                reader.Close();
            } 
            return drinks;
        }
        public void CreateDrinks(DrinksModel drink)
        {
            var connection = _connection;
            connection.Open();
            string query = "INSERT INTO drinks (food_name, price, photo) " +
                           "VALUES (@name, @price, @photo)";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@name", drink.Name);
            command.Parameters.AddWithValue("@price", Math.Round(drink.Price, 2));
            command.Parameters.Add("@photo", MySqlDbType.Blob).Value = drink.Photo;
            command.ExecuteNonQuery();
            connection.Close();
        }
        
        public void DeleteDrinks(int id)
        {
            var connection = _connection;
            connection.Open();
            string query = "DELETE FROM drinks WHERE id = @drinksId;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@drinksId", id);

            int rowsAffected = command.ExecuteNonQuery();
            if (rowsAffected == 0)
            {
                throw new Exception($"No drinks with ID {id} found in database.");
            }
            connection.Close();
        }
    }
}
