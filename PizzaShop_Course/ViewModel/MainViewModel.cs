using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class MainViewModel : UserViewModel
    {
        private UserModel user;
        public UserModel User
        {
            get => user;
            set
            {
                if(user!=value)
                user = value;
                OnPropertyChanged(nameof(User));
            }
        }
        private bool isLoggedIn = UserViewModel.IsAuthorized;
        public bool IsLoggedIn
        {
            get => isLoggedIn;
            set
            {
                isLoggedIn = value;
                OnPropertyChanged(nameof(IsAuthorized));
            }
        }
        public MainViewModel()
        {
            ToggleHamburgerCommand = new RelayCommand(param => IsHamburgerOpen = !IsHamburgerOpen);
            NavigateCommand = new RelayCommand<Type>(NavigateTo);
            //AddToBasketCommand = new RelayCommand(AddToBasket);
            User = UserViewModel.CurrentUser;
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

        public ICommand NavigateCommand { get; set; }
        public void Navigate(UserControl view)
        {
            CurrentView = view;
        }
        public void NavigateTo(Type viewType)
        {
            UserControl view = (UserControl)Activator.CreateInstance(viewType);
            Navigate(view);
        }


        #region basket


        public string BasketTotalPrice
        {
            get => BasketViewModel.TotalPrice;
            set
            {
                BasketViewModel.TotalPrice = value;
                OnPropertyChanged(nameof(BasketTotalPrice));
            }

        }
        #endregion
        #region logout

        #endregion
    }
}
