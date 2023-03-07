using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces;
using System.Data.Common;

namespace PizzaShop_Course.Model
{
    public class UserModel : PropertyBase, IHumanInformation
    {
        private readonly MySqlConnection connection = SqlDBConnection.GetDBConnection();

        private bool changeRoots;
        public bool ChangeRoots
        {
            get => changeRoots;
            set
            {
                changeRoots = value;
                OnPropertyChanged();
            }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged();
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged();
            }
        }

        private byte[]? photopath = null;
        public byte[]? PhotoPath
        {
            get => photopath;
            set
            {
                photopath = value;
                OnPropertyChanged();
            }
        }

        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        private string? email;
        public string? Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        //public UserModel Authenticate(string username, string password)
        //{
        //    UserModel user = null;
        //    string query = "SELECT * FROM users WHERE username = @username AND password = @password";
        //    using (MySqlCommand cmd = new MySqlCommand(query, connection))
        //    {
        //        cmd.Parameters.AddWithValue("@username", username);
        //        cmd.Parameters.AddWithValue("@password", password);
        //        connection.Open();
        //        using (MySqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            if (reader.Read())
        //            {
        //                user = new UserModel
        //                {
        //                    Id = reader.GetInt32("id"),
        //                    ChangeRoots = reader.GetBoolean("change_roots"),
        //                    FirstName = reader.GetString("first_name"),
        //                    LastName = reader.GetString("last_name"),
        //                    PhotoPath = (byte[])reader["photo"],
        //                    Login = reader.GetString("login"),
        //                    Password = reader.GetString("password"),
        //                    Email = reader.GetString("email")
        //                };
        //                return user;
        //            }
        //        }
        //    }
        //    return null;
        //}

    }
}
