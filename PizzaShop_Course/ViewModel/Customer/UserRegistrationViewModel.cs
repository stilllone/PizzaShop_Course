using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using PizzaShop_Course.View.Customer;
using PizzaShop_Course.ViewModel.Administrator;
using PizzaShop_Course.ViewModel.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PizzaShop_Course.ViewModel.Customer
{
    public class UserRegistrationViewModel : PropertyBase
    {
        private UserDBConnection userDBConnection;
        private const string PasswordRegexPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        private readonly Regex passwordRegex = new Regex(PasswordRegexPattern);
        public UserRegistrationViewModel()
        {
            userDBConnection = new UserDBConnection();
        }
        private ImageSource userPhoto;
        public ImageSource UserPhoto
        {
            get => userPhoto;
        }
        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        private string number;
        public string Number
        {
            get { return number; }
            set
            {
                if (number != value)
                {
                    number = value;
                    OnPropertyChanged(nameof(Number));
                }
            }
        }
        private bool changeRoots;
        public bool ChangeRoots
        {
            get { return changeRoots; }
            set
            {
                if (changeRoots != value)
                {
                    changeRoots = value;
                    OnPropertyChanged(nameof(ChangeRoots));
                }
            }
        }
        private string firstName;
        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }
        private string lastName;
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }
        private byte[] photo;
        public byte[] Photo
        {
            get { return photo; }
            set
            {
                if (photo != value)
                {
                    photo = value;
                    OnPropertyChanged(nameof(Photo));
                }
            }
        }
        private string login;
        public string Login
        {
            get { return login; }
            set
            {
                if (login != value)
                {
                    login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }
        private string password;
        [Required(ErrorMessage = "Password is required.")]
        [CustomValidation(typeof(EmailValidation), "IsValid", ErrorMessage = "Password is not valid.")]
        public string Password
        {
            get { return password; }
            set
            {
                if (!passwordRegex.IsMatch(value))
                {
                    EventAggregator.Instance.NotificationEvent.Publish("Invalid password. Password must be at least 8 characters long and contain only Latin letters.");
                }
                if (password != value)
                {
                    password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        private string email;
        [Required(ErrorMessage = "Email is required.")]
        [CustomValidation(typeof(EmailValidation), "IsValid", ErrorMessage = "Email is not valid.")]
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        public ICommand CreateRegistrationWindowCommand { get => new RelayCommand(CreateRegistrationWindow); }
        public ICommand CreateUserAndAuthorizeCommand { get => new RelayCommand<object>(CreateAndAuthorize); }
        private UserModel CreateUserModel()
        {
            
            if (FirstName == null)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Input FirstName");
                return null;
            }
            else if (LastName == null)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Input LastName");
                return null;
            }
            else if (Login == null && Password == null)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Input correct Login and pasword");
                return null;
            }
            else if (Email == null && Number == null)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Invalid number or Email");
                return null;
            }
            var user = new UserModel();
            user.Login = this.Login;
            user.Password = this.Password;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Email = this.Email;
            user.Number = this.Number;
            user.Photo = this.Photo;
            user.ChangeRoots = this.ChangeRoots;
            return user;
        }
        private bool SaveUser(object parameter)
        {
            return userDBConnection.CreateUser(CreateUserModel());
        }
        private void AuthorizeUser(object parameter)
        {
            UserViewModel.CurrentUser = userDBConnection.AuthenticateUser(Login, Password);
            if (CreateUserModel == null)
            {
                UserViewModel.IsAuthorized = false;
                EventAggregator.Instance.NotificationEvent.Publish("Uncorrect input data");
            }
            else
            {
                UserViewModel.IsAuthorized = true;
            }
            OnPropertyChanged(nameof(UserViewModel.IsAuthorized));
            OnPropertyChanged(nameof(UserViewModel.CurrentUser));
        }
        private void CreateRegistrationWindow(object parameter)
        {
            Window regWindow = new UserCreateView();
            regWindow.ShowDialog();
            CreateUserAndAuthorizeCommand.Execute(regWindow);
        }
        private void CreateAndAuthorize(object parameter)
        {
            try
            {
                if (SaveUser(CreateUserModel()) != false)
                {
                    AuthorizeUser(null);
                    var result = MessageBox.Show("Congratulations, you are registered", "Registration Successful", MessageBoxButton.OK);
                    {
                        if (result == MessageBoxResult.OK)
                        {
                            var regWindow = parameter as Window;
                            if (regWindow != null)
                            {
                                regWindow.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Something went wrong, try later");
                Debug.WriteLine(ex);
            }
        }
    }
}
