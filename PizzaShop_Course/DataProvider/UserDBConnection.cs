using MySql.Data.MySqlClient;
using PizzaShop_Course.Model;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                string query = "INSERT INTO users (change_roots, first_name, last_name, photo, login, password, email) " +
                           "VALUES (@changeRoots, @firstName, @lastName, @photo, @login, @password, @email)";
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@changeRoots", user.ChangeRoots);
                command.Parameters.AddWithValue("@firstName", user.FirstName);
                command.Parameters.AddWithValue("@lastName", user.LastName);
                command.Parameters.AddWithValue("@photo", user.PhotoPath);
                command.Parameters.AddWithValue("@login", user.Login);
                command.Parameters.AddWithValue("@password", user.Password);
                command.Parameters.AddWithValue("@email", user.Email);
                command.ExecuteNonQuery();
            }
        }

        public UserModel AuthenticateUser(string login, string password)
        {
            using (var connection = _connection)
            {
                connection.Open();
                var query = "SELECT * FROM users WHERE login=@login";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@login", login);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var hashedPassword = reader.GetString("password");
                            if (PasswordHasher.VerifyPassword(password, hashedPassword))
                            {
                                return new UserModel
                                {
                                    Id = reader.GetInt32("id"),
                                    ChangeRoots = reader.GetBoolean("change_roots"),
                                    FirstName = reader.GetString("first_name"),
                                    LastName = reader.GetString("last_name"),
                                    PhotoPath = (byte[])reader["photo"],
                                    Login = reader.GetString("login"),
                                    Password = hashedPassword,
                                    Email = reader.GetString("email")
                                };
                            }
                        }
                    }
                }
                connection.Close();
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
