using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Data.Common;
using System.IO;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PizzaShop_Course.Model
{
    public class UserModel : PropertyBase, IHumanInformation
    {
        private bool changeRoots;
        public bool ChangeRoots
        {
            get => changeRoots;
            set
            {
                changeRoots = value;
                OnPropertyChanged(nameof(ChangeRoots));
            }
        }

        private string firstName;
        public string FirstName
        {
            get => firstName;
            set
            {
                firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string lastName;
        public string LastName
        {
            get => lastName;
            set
            {
                lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private byte[]? photo = null;
        public byte[]? Photo
        {
            get => photo;
            set
            {
                photo = value;
                OnPropertyChanged(nameof(Photo));
            }
        }

        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string? email;
        public string? Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string number;
        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged(nameof(Number));
            }
        }

        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
            }
        }
    }
}
