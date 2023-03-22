using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using PizzaShop_Course.View;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PizzaShop_Course.ViewModel
{
    public class MainViewModel : PropertyBase
    {
        public MainViewModel()
        {
            ToggleHamburgerCommand = new RelayCommand(param => IsHamburgerOpen = !IsHamburgerOpen);
            User = UserViewModel.CurrentUser;
            BasketViewModel.OrderItems.CollectionChanged += OrderItems_CollectionChanged;
            UserViewModel.UserChanged += OnUserChanged;
            UserViewModel.AuthorizeChanged += OnAuthorizeChanged;
        }
        private void OrderItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
            {
                BasketTotalPrice = BasketViewModel.OrderItems.Sum(item => item.ItemPrice);
            }
        }
        private void OnUserChanged(object sender, UserModel newUser)
        {
            User = UserViewModel.CurrentUser;
        }
        private void OnAuthorizeChanged(object sender, bool newAuthorize)
        {
            IsLoggedIn = UserViewModel.IsAuthorized;
        }
        private UserModel user;
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

        private ImageSource userPhoto;
        public ImageSource UserPhoto
        {
            get => userPhoto;
        }
        private bool isLoggedIn = UserViewModel.IsAuthorized;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set
            {
                isLoggedIn = value;
                UserControl us = new UserInformationView();
                if (isLoggedIn == true)
                {
                    Navigate(us);
                }
                else
                {
                    UserControl pizzaView = new PizzasView();
                    Navigate(pizzaView);
                }
                Debug.WriteLine("IsLoggedIn Changed: " + isLoggedIn);
                OnPropertyChanged(nameof(IsLoggedIn));
            }
        }
        
        public ICommand ToggleHamburgerCommand { get; }

        private bool _isHamburgerOpen;
        public bool IsHamburgerOpen
        {
            get { return _isHamburgerOpen; }
            set
            {
                if (_isHamburgerOpen != value)
                {
                    _isHamburgerOpen = value;
                    OnPropertyChanged(nameof(IsHamburgerOpen));
                }
            }
        }

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set
            {
                if (_currentView != value)
                {
                    _currentView = value;
                    OnPropertyChanged(nameof(CurrentView));
                }
            }
        }

        public ICommand NavigateCommand { get => new RelayCommand<Type>(NavigateTo); }
        private void Navigate(UserControl view)
        {
            CurrentView = view;
        }
        private void NavigateTo(Type viewType)
        {
            UserControl view = (UserControl)Activator.CreateInstance(viewType);
            Navigate(view);
        }


        #region basket
        private double baskettotalprice = BasketViewModel.OrderItems.Sum(item => item.ItemPrice);
        public double BasketTotalPrice
        {
            get => baskettotalprice;
            set
            {
                baskettotalprice = value;
                OnPropertyChanged(nameof(baskettotalprice));
            }
        }
        
        #endregion
        #region logout

        #endregion
    }
}
