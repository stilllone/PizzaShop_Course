using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PizzaShop_Course.Model
{
    public class DrinksModel : PropertyBase, IFood
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
            get => size;
            set
            {
                size = value;
                OnPropertyChanged();
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
            BasketViewModel.OrderItems.Add(new BasketItemModel()
            {
                ItemId = this.id,
                ItemName = this.name,
                ItemPrice = this.price,
                ItemSize = this.size,
                ItemPhoto = this.photo
            });
            OnGlobalPropertyChanged(nameof(BasketViewModel.OrderItems));
            Debug.WriteLine(BasketViewModel.OrderItems.Count);
        }
        
    }
}
