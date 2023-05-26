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
        private PizzasModel pizza;
        public PizzasModel Pizza
        {
            get => pizza = new PizzasModel()
            {
                Id = Id,
                Name = this.Name,
                Size = this.Size,
                Price = this.Price,
                Photo = this.Photo
            };
            set
            {
                if (pizza != value)
                    pizza = value;
                OnPropertyChanged(nameof(Pizza));
            }
        }
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
                    Price = firstValueOfPrice;
                }
                else if (Size == FoodSize.small.ToString())
                {
                    Price = Math.Round(firstValueOfPrice * 1.15, 2);
                }
                else
                {
                    Price = Math.Round(firstValueOfPrice * 1.25, 2);
                }
                OnPropertyChanged(nameof(Size));
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName == nameof(Size))
            {
                OnPropertyChanged(nameof(Pizza));
            }
        }
    }
}
