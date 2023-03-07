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
using System.Windows;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class LoginViewModel : PropertyBase
    {

        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                login = value;
                OnPropertyChanged(nameof(login));
            }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private bool isLoggedIn;
        public bool IsLoggedIn
        {
            get { return isLoggedIn; }
            set
            {
                isLoggedIn = value;
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }

        private ICommand loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                if (loginCommand == null)
                {
                    loginCommand = new RelayCommand(UserLogin);
                }
                return loginCommand;
            }
        }

        private void UserLogin(object parameter)
        {
            UserModel user = null;
            using (MySqlConnection connection = SqlDBConnection.GetDBConnection())
            {
                connection.Open();
                MySqlCommand command = new MySqlCommand("SELECT * FROM UserAccounts WHERE login = @Login, password = @Password", connection);
                command.Parameters.AddWithValue("@login", Login);
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

            
            if (user != null)
            {
                IsLoggedIn = true;
            }
            else
            {
                MessageBox.Show("Incorrect Login or Password");
            }
        }

    }
}
