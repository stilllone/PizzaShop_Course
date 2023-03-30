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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace PizzaShop_Course.ViewModel.Customer
{
    public class UserRegistrationViewModel : PropertyBase
    {
        private UserDBConnection userDBConnection;
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
        public string Password
        {
            get { return password; }
            set
            {
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
            var user = new UserModel();
            user.Login = this.Login;
            user.Password = this.Password;
            if (Login == null && Password == null)
                return user = null;
            user.FirstName = this.FirstName;
            user.LastName = this.LastName;
            user.Email = this.Email;
            user.Photo = this.Photo;
            user.ChangeRoots = this.ChangeRoots;
            return user;
            
        }
        private void SaveUser(object parameter)
        {
            userDBConnection.CreateUser(CreateUserModel());
        }
        private void AuthorizeUser(object parameter)
        {
            UserViewModel.CurrentUser = userDBConnection.AuthenticateUser(CreateUserModel().Login, CreateUserModel().Password);
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
                SaveUser(CreateUserModel());
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
            catch (Exception ex)
            {
                EventAggregator.Instance.NotificationEvent.Publish("Something went wrong, try later");
                Debug.WriteLine(ex);
            }
        }
    }
}
