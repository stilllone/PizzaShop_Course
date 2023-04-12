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
        
        private DrinksModel drink;
        public DrinksModel Drink
        {
            get => drink = new DrinksModel()
            {
                Id = this.Id,
                Name = this.Name,
                Size = this.Size,
                Price = this.Price,
                Photo = this.Photo
            };
            set
            {
                if (drink != value)
                    drink = value;
                OnPropertyChanged(nameof(Drink));
            }
        }
        
        private int id;
        public int Id
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged(nameof(Id));
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
        private double firstValueOfPrice;
        public double Price
        {
            get => price;
            set
            {
                
                price = value;
                OnPropertyChanged(nameof(Price));
                if (firstValueOfPrice == 0)
                {
                    firstValueOfPrice = price;
                }
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
                    Price = firstValueOfPrice;
                    OnPropertyChanged(nameof(Price));
                }
                else if (Size == FoodSize.small.ToString())
                {
                    Price = firstValueOfPrice * 1.15;
                    OnPropertyChanged(nameof(Price));
                }
                else
                {
                    Price = firstValueOfPrice * 1.25;
                    OnPropertyChanged(nameof(Price));
                }
                OnPropertyChanged(nameof(Size));
            }
        }
        public FoodSize[] DrinksSize
        {
            get { return (FoodSize[])Enum.GetValues(typeof(FoodSize)); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Size))
            {
                OnPropertyChanged(nameof(Drink));
            }
        }
    }
}
