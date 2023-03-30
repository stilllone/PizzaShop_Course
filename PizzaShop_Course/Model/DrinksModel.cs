using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PizzaShop_Course.Model
{
    public class DrinksModel : AbsFood, INotifyPropertyChanged
    {
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged();
            }
        }
        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private double price;
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged();
            }
        }

        private byte[] photo;
        public byte[] Photo
        {
            get => photo;
            set
            {
                photo = value;
                OnPropertyChanged();
            }
        }

        private string size;
        public string Size
        {
            get { return size; }
            set
            {
                size = value;
                if (size == FoodSize.little.ToString())
                {
                    Price = Price;
                    OnPropertyChanged(nameof(Price));
                }
                else if (Size == FoodSize.small.ToString())
                {
                    Price *= 1.15;
                    OnPropertyChanged(nameof(Price));
                }
                else
                {
                    Price *= 1.25;
                    OnPropertyChanged(nameof(Price));
                }
                OnPropertyChanged(nameof(Size));
            }
        }
        public FoodSize[] DrinksSize
        {
            get { return (FoodSize[])Enum.GetValues(typeof(FoodSize)); }
        }
        //Commands (idk how else)
        public ICommand AddDrinksToBasket
        {
            get => new RelayCommand(AddDrinks);
            set
            {
                AddDrinksToBasket?.Execute(value);
            }
        }
        private void AddDrinks(object drinks)
        {
            var newItem = new BasketItemModel()
            {
                ItemId = this.id,
                ItemName = this.name,
                ItemPrice = this.price,
                ItemSize = this.size,
                ItemPhoto = this.photo,
                ItemCount = 1
            };
            var existingItem = BasketViewModel.OrderItems.FirstOrDefault(item =>
                item.ItemName == newItem.ItemName && item.ItemSize == newItem.ItemSize);
            if (existingItem != null)
            {
                existingItem.ItemCount++;
            }
            else
            {
                BasketViewModel.OrderItems.Add(newItem);
            }
            OnGlobalPropertyChanged(nameof(BasketViewModel.OrderItems));
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        private static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { }; //update static property
        protected static void OnGlobalPropertyChanged(string propertyName)
        {
            GlobalPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
