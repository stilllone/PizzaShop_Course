using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class LoginViewModel : PropertyBase
    {

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                _isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (_loginCommand == null)
                {
                    _loginCommand = new RelayCommand(Login);
                }
                return _loginCommand;
            }
        }

        private void Login(object parameter)
        {
            UserModel user = null;
            using (MySqlConnection connection = SqlDBConnection.GetDBConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM UserAccounts WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", Username);
                MySqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    user = new UserModel
                    {
                        FirstName = (string)reader[""],
                        LastName = (string)reader[""],
                        ChangeRoots = (bool)reader["Id"],
                        Login = (string)reader["Username"],
                        Password = (string)reader["Password"],
                        Email = (string)reader["Email"],
                        PhotoPath = (byte[])reader[""]
                    };
                }
            }

            
            if (user != null && PasswordHasher.VerifyPassword(Password, user.Password))
            {
                IsLoggedIn = true;
                // Navigate to the mainview
            }
            else
            {
                //error message
            }
        }

    }
}
