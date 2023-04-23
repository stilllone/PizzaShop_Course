using Microsoft.Win32;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using System.Windows;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.IO;
using System.ComponentModel;
using System.Windows.Controls;
using System.Diagnostics;
using PizzaShop_Course.View;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using PizzaShop_Course.View.Customer;
using System.ComponentModel.DataAnnotations;
using PizzaShop_Course.ViewModel.Validation;
using System.Linq;
using System.Text.RegularExpressions;

namespace PizzaShop_Course.ViewModel.Administrator
{
    
    public class UserViewModel : PropertyBase
    {
        private UserDBConnection userDBConnection;
        public delegate void UserChangedEventHandler(object sender, UserModel newUser);
        public static event UserChangedEventHandler UserChanged;
        public delegate void AuthorizeInfChangedEventHandler(object sender, bool newStatus);
        public static event AuthorizeInfChangedEventHandler AuthorizeChanged;

        private const string PasswordRegexPattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";
        private readonly Regex passwordRegex = new Regex(PasswordRegexPattern);

        public UserViewModel()
        {
            userDBConnection = new UserDBConnection();
            User = CurrentUser;
            try
            {
                CurrentUser.PropertyChanged += OnCurrentUserPropertyChanged;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("Ошибка NullReferenceException: {0}", ex.Message);
            }
        }
        #region property
        private void OnCurrentUserPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(UserModel.Photo))
            {
                if (CurrentUser != null && CurrentUser.Photo != null)
                {
                    var imageSource = new BitmapImage();
                    using (var stream = new MemoryStream(CurrentUser.Photo))
                    {
                        imageSource.BeginInit();
                        imageSource.CacheOption = BitmapCacheOption.OnLoad;
                        imageSource.StreamSource = stream;
                        imageSource.EndInit();
                    }
                    UserPhoto = imageSource;
                }
                else
                {
                    UserPhoto = null;
                }
            }
        }
        private static UserModel user = new UserModel();
        public UserModel User
        {
            get => user;
            set
            {
                if (user != value)
                {
                    user = value;
                    OnPropertyChanged(nameof(User));

                    if (user != null && user.Photo != null)
                    {
                        var imageSource = new BitmapImage();
                        using (var stream = new MemoryStream(user.Photo))
                        {
                            imageSource.BeginInit();
                            imageSource.CacheOption = BitmapCacheOption.OnLoad;
                            imageSource.StreamSource = stream;
                            imageSource.EndInit();
                        }
                        userPhoto = imageSource;
                        OnPropertyChanged(nameof(UserPhoto));
                    }
                    else
                    {
                        userPhoto = null;
                        OnPropertyChanged(nameof(UserPhoto));
                    }
                }
            }
        }
        public static UserModel CurrentUser
        {
            get => user;
            set
            {
                user = value;
                UserChanged?.Invoke(null, user);
            }
        }
        private ImageSource userPhoto;
        public ImageSource UserPhoto
        {
            get => userPhoto;
            set
            {
                userPhoto = value;
                OnPropertyChanged(nameof(UserPhoto));
            }
        }
        private static bool isAuthorized;
        public static bool IsAuthorized
        {
            get => isAuthorized;
            set
            {
                isAuthorized = value;
                AuthorizeChanged?.Invoke(null, isAuthorized);
                OnGlobalPropertyChanged(nameof(isAuthorized));
            }
        }
        public int Id
        {
            get { return user.Id; }
            set
            {
                if (user.Id != value)
                {
                    user.Id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        public string Number
        {
            get { return user.Number; }
            set
            {
                if (user.Number != value)
                {
                    user.Number = value;
                    OnPropertyChanged(nameof(Number));
                }
            }
        }

        public bool ChangeRoots
        {
            get { return user.ChangeRoots; }
            set
            {
                if (user.ChangeRoots != value)
                {
                    user.ChangeRoots = value;
                    OnPropertyChanged(nameof(ChangeRoots));
                }
            }
        }

        public string FirstName
        {
            get { return user.FirstName; }
            set
            {
                if (user.FirstName != value)
                {
                    user.FirstName = value;
                    OnPropertyChanged(nameof(FirstName));
                }
            }
        }

        public string LastName
        {
            get { return user.LastName; }
            set
            {
                if (user.LastName != value)
                {
                    user.LastName = value;
                    OnPropertyChanged(nameof(LastName));
                }
            }
        }

        public byte[] Photo
        {
            get { return user.Photo; }
            set
            {
                if (user.Photo != value)
                {
                    user.Photo = value;
                    OnPropertyChanged(nameof(Photo));
                }
            }
        }

        public string Login
        {
            get { return user.Login; }
            set
            {
                if (user.Login != value)
                {
                    user.Login = value;
                    OnPropertyChanged(nameof(Login));
                }
            }
        }

        public string Password
        {
            get { return user.Password; }
            set
            {
                if (!passwordRegex.IsMatch(value))
                {
                    EventAggregator.Instance.NotificationEvent.Publish("Invalid password. Password must be at least 8 characters long and contain only Latin letters.");
                }
                else if (user.Password != value)
                {
                    user.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }
        [Required(ErrorMessage = "Email is required.")]
        [CustomValidation(typeof(EmailValidation), "IsValid", ErrorMessage = "Email is not valid.")]
        public string Email
        {
            get { return user.Email; }
            set
            {
                if (user.Email != value)
                {
                    user.Email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }
        private string photopath;
        public string PhotoPath
        {
            get => photopath;
            set
            {
                photopath = value;
                OnPropertyChanged(nameof(PhotoPath));
            }
        }

        #endregion
        #region command
        public ICommand AuthorizeCommand => new RelayCommand(AuthorizeUser);
        public ICommand LogOutCommand => new RelayCommand(LogOut);
        private void AuthorizeUser(object parameter)
        {
            var possibleUser = userDBConnection.AuthenticateUser(user.Login, user.Password);
            
            if (possibleUser == null)
            {
                IsAuthorized = false;
                EventAggregator.Instance.NotificationEvent.Publish("Uncorrect input data");
            }
            else
            {
                CurrentUser = possibleUser; 
                IsAuthorized = true;
            }
            OnPropertyChanged(nameof(IsAuthorized));
            OnPropertyChanged(nameof(AuthorizeCommand));
            OnPropertyChanged(nameof(CurrentUser));
        }
        public ICommand SaveCommand { get => new RelayCommand(SaveUser); }
        public ICommand UpdateCommand { get => new RelayCommand(UpdateUser); }
        public ICommand DeleteCommand { get => new RelayCommand(DeleteUser); }
        public ICommand SelectPhotoCommand { get => new RelayCommand(SelectPhoto); }
        public ICommand CreateRegistrationWindowCommand { get => new RelayCommand(CreateRegistrationWindow); }

        private void SaveUser(object parameter)
        {
            try
            {
                userDBConnection.CreateUser(user);
            }
            catch
            {
                EventAggregator.Instance.NotificationEvent.Publish("You can't do this");
            }
        }

        private void UpdateUser(object parameter)
        {
            if (CurrentUser.ChangeRoots == true)
                userDBConnection.UpdateUser(user);
            else
                userDBConnection.UpdateByUser(user);
        }
        

        private void DeleteUser(object parameter)
        {
            userDBConnection.DeleteUser(user.Id);
        }
        
        private void SelectPhoto(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Photo = File.ReadAllBytes(openFileDialog.FileName);
                PhotoPath = openFileDialog.FileName;
            }
        }
        private void LogOut(object parameter)
        {
            CurrentUser = null;
            user = CurrentUser;
            IsAuthorized = false;
        }
        private void CreateRegistrationWindow(object parameter)
        {
            Window regWindow = new UserCreateView();
            regWindow.ShowDialog();
        }
        #endregion
    }
}
