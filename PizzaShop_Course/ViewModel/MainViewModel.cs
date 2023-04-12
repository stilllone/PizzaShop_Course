using CommonServiceLocator;
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
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

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
            EventAggregator.Instance.NotificationEvent.Subscribe(ShowNotification);
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
            set
            {
                userPhoto = value;
                OnPropertyChanged(nameof(UserPhoto));
            }
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
                };
                OnPropertyChanged(nameof(IsLoggedIn));
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
        #region commands
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

        private UserControl _currentView = new PizzasView();
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
        #endregion

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
        private void Item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BasketItemModel.ItemPrice) || e.PropertyName == nameof(BasketItemModel.ItemCount))
            {
                BasketTotalPrice = BasketViewModel.OrderItems.Sum(item => item.ItemPrice * item.ItemCount);
            }
        }
        private void OrderItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var item in e.NewItems.OfType<BasketItemModel>())
                {
                    item.PropertyChanged += Item_PropertyChanged;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var item in e.OldItems.OfType<BasketItemModel>())
                {
                    item.PropertyChanged -= Item_PropertyChanged;
                }
            }
            if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Remove)
            {
                BasketTotalPrice = BasketViewModel.OrderItems.Sum(item => item.ItemPrice * item.ItemCount);
            }

            RecalculateBasketTotalPrice();
        }
        private void RecalculateBasketTotalPrice()
        {
            BasketTotalPrice = BasketViewModel.OrderItems.Sum(item => (item.ItemPrice * item.ItemCount));
        }
        #endregion
        #region popup

        private UserControl notificationView;
        public UserControl NotificationView 
        {
            get => notificationView;
            set
            {
                notificationView = value;
                OnPropertyChanged(nameof(notificationView));
                Debug.WriteLine("NotificationView Changed");
            }
        }
        private void ShowNotification(string message)
        {
            var notificationView = new Notification();
            var notificationViewModel = new NotificationViewModel();
            notificationViewModel.SetNotificationText(message);
            notificationView.DataContext = notificationViewModel;
            NotificationView = notificationView;
        }
        #endregion
    }
}
