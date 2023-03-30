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
using System.Windows.Controls;
using System.Windows.Input;

namespace PizzaShop_Course.Model
{
    public class PizzasModel : AbsFood, INotifyPropertyChanged
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

        private string ingridients;
        public string Ingredients
        {
            get => ingridients;
            set
            {
                ingridients = value;
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

        private double price;
        public double Price
        {
            get => price;
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
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

        private int mass;
        public int Mass
        {
            get => mass;
            set
            {
                mass = value;
                OnPropertyChanged();
            }
        }
        public FoodSize[] PizzaSize
        {
            get { return (FoodSize[])Enum.GetValues(typeof(FoodSize)); }
        }

        public ICommand AddPizzaToBasket
        {
            get => new RelayCommand(AddPizza);
            set
            {
                AddPizzaToBasket?.Execute(value);
            }
        }
        private void AddPizza(object drinks)
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
        private static event PropertyChangedEventHandler GlobalPropertyChanged = delegate { };
        protected static void OnGlobalPropertyChanged(string propertyName)
        {
            GlobalPropertyChanged(null, new PropertyChangedEventArgs(propertyName));
        }
    }
}
