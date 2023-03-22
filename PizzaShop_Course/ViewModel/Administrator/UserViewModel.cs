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

namespace PizzaShop_Course.ViewModel.Administrator
{
    
    public class UserViewModel : PropertyBase
    {
        private UserDBConnection userDBConnection;
        
        
        public delegate void UserChangedEventHandler(object sender, UserModel newUser);
        public static event UserChangedEventHandler UserChanged;
      
        public UserViewModel()
        {
            userDBConnection = new UserDBConnection();
            user = new UserModel();
            AuthorizeCommand = new RelayCommand<Type>(AuthorizeUser);
        }

        #region property
        private static UserModel user;
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
        }
        private static bool isAuthorized = false;
        public static bool IsAuthorized
        {
            get => isAuthorized;
            set
            {
                isAuthorized = value;
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
                if (user.Password != value)
                {
                    user.Password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

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
       
        #endregion 
        private ICommand authorizeCommand;
        public ICommand AuthorizeCommand
        { 
            get => authorizeCommand;
            set
            {
                if (isAuthorized == false)
                {
                    authorizeCommand = new RelayCommand(AuthorizeUser);
                    OnPropertyChanged(nameof(AuthorizeCommand));
                }
                else
                {
                    authorizeCommand = new RelayCommand(LogOut);
                    OnPropertyChanged(nameof(AuthorizeCommand));
                }
            }
        }
        private void AuthorizeUser(object parameter)
        {
            CurrentUser = userDBConnection.AuthenticateUser(user.Login, user.Password);
            if (user == null)
            {
                IsAuthorized = false;
                MessageBox.Show("Uncorrect input data");
            }
            else
            {
                IsAuthorized = true;
            }
            OnPropertyChanged(nameof(CurrentUser));
        }
        public ICommand SaveCommand { get => new RelayCommand(SaveUser); }
        public ICommand UpdateCommand { get => new RelayCommand(UpdateUser); }
        public ICommand DeleteCommand { get => new RelayCommand(DeleteUser); }
        public ICommand SelectPhotoCommand { get => new RelayCommand(SelectPhoto); }

        private void SaveUser(object parameter)
        {
            userDBConnection.CreateUser(user);
        }

        private void UpdateUser(object parameter)
        {
            userDBConnection.UpdateUser(user);
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
            }
        }
        private void LogOut(object parameter)
        {
            user = null;
        }
    }
}
