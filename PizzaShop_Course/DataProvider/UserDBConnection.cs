using MySql.Data.MySqlClient;
using PizzaShop_Course.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;

namespace PizzaShop_Course.DataProvider
{
    public class UserDBConnection
    {
        private readonly MySqlConnection _connection;

        public UserDBConnection()
        {
            _connection = SqlDBConnection.GetDBConnection();
        }
        public void CreateUser(UserModel user)
        {
            using (var connection = _connection)
            {
                connection.Open();
                string query = "INSERT INTO users (change_roots, first_name, last_name, photo, login, pass, email) " +
                           "VALUES (@changeRoots, @firstName, @lastName, @photo, @login, @pass, @email)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@changeRoots", user.ChangeRoots);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@photo", user.PhotoPath);
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@pass", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public UserModel AuthenticateUser(string login, string password)
        {
            UserModel user = null;
            using (MySqlConnection connection = SqlDBConnection.GetDBConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM users WHERE login = @login AND pass = @pass", connection);
                command.Parameters.AddWithValue("@login", login);
                command.Parameters.AddWithValue("@pass", password);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserModel
                    {
                        Id = (int)reader["id"],
                        FirstName = (string)reader["first_name"],
                        LastName = (string)reader["last_name"],
                        ChangeRoots = (bool)reader["change_roots"],
                        Login = (string)reader["login"],
                        Password = (string)reader["pass"],
                        Email = (reader["email"] == DBNull.Value) ? string.Empty : (string?)reader["email"],
                        PhotoPath = (reader["photo"] == DBNull.Value) ? null : (byte[]?)reader["photo"]
                    };
                    connection.Close();
                    return user;
                }
            }
            return null;
        }
        public void AddUser(UserModel user)
        {
            using (var connection = _connection)
            {
                connection.Open();
                var query = "INSERT INTO users (change_roots, first_name, last_name, photo, login, password, email) VALUES (@ChangeRoots, @FirstName, @LastName, @Photo, @Login, @Password, @Email)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@change_roots", user.ChangeRoots);
                command.Parameters.AddWithValue("@first_name", user.FirstName);
                command.Parameters.AddWithValue("@last_name", user.LastName);
                command.Parameters.AddWithValue("@photo", user.PhotoPath);
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.ExecuteNonQuery();
            }
        }
        public void DeleteUser(int id)
        {
            using (var connection = _connection)
            {
                string query = "DELETE FROM users WHERE id=@id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
        public void UpdateUser(UserModel user)
        {
            using (var connection = _connection)
            {
                var query = "UPDATE users SET change_roots = @ChangeRoots, first_name = @FirstName, last_name = @LastName, photo = @Photo, password = @Password, email = @Email WHERE id = @Id";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@change_roots", user.ChangeRoots);
                command.Parameters.AddWithValue("@first_name", user.FirstName);
                command.Parameters.AddWithValue("@last_name", user.LastName);
                command.Parameters.AddWithValue("@photo", user.PhotoPath);
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.ExecuteNonQuery();
            }
        }
    }
}
