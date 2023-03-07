using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private Image userphoto;
        public Image UserPhoto
        {
            get => userphoto;
            set
            {
                userphoto = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(UserPhoto)));
            }
        }

        public MainViewModel()
        {
            ToggleHamburgerCommand = new RelayCommand(param => IsHamburgerOpen = !IsHamburgerOpen);
            NavigateCommand = new RelayCommand<Type>(NavigateTo);
            AddToBasketCommand = new RelayCommand(AddToBasket);
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsHamburgerOpen)));
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
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CurrentView)));
                }
            }
        }

        public ICommand NavigateCommand { get; }
        public void Navigate(UserControl view)
        {
            CurrentView = view;
        }
        private void NavigateTo(Type viewType)
        {
            UserControl view = (UserControl)Activator.CreateInstance(viewType);
            Navigate(view);
        }


        #region basket
        private BasketModel _basket = new BasketModel();
        public BasketModel Basket
        {
            get { return _basket; }
            set
            {
                _basket = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Basket)));
            }
        }

        public ICommand AddToBasketCommand { get; }
        private void AddToBasket(object parameter)
        {
            if (parameter is PizzasModel item)
            {
                Basket.Items.Add(item);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Basket)));
            }
        }
        #endregion
        #region logout



        #endregion
    }
}
