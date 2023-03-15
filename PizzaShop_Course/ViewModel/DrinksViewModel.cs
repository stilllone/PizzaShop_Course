using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class DrinksViewModel : PropertyBase
    {

        //private DrinksModel drink;
        public DrinksViewModel()
        {
            DrinksDBConnection drinksModel = new DrinksDBConnection();
            drinks = drinksModel.GetDrinks();
            AddDrinksToBasket = new RelayCommand(AddDrinks);
            DeleteDrinksFromBasket = new RelayCommand(DeleteDrinks);
        }

        //#region prop
        //public int Id
        //{
        //    get => drink.Id;
        //    set
        //    {
        //        drink.Id = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public string Name
        //{
        //    get => drink.Name;
        //    set
        //    {
        //        drink.Name = value;
        //        OnPropertyChanged();
        //    }
        //}
        //public double Price
        //{
        //    get => drink.Price;
        //    set
        //    {
        //        drink.Price = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public byte[] Photo
        //{
        //    get => drink.Photo;
        //    set
        //    {
        //        drink.Photo = value;
        //        OnPropertyChanged();
        //    }
        //}

        //public string Size
        //{
        //    get => drink.Size;
        //    set
        //    {
        //        drink.Size = value;
        //        OnPropertyChanged();
        //    }
        //}
        //#endregion
        private ObservableCollection<DrinksModel> drinks;
        public ObservableCollection<DrinksModel> Drinks
        {
            get => drinks;
            set
            {
                drinks = value;
                OnPropertyChanged(nameof(Drinks));
            }
        }
        public ICommand AddDrinksToBasket { get; }
        public ICommand DeleteDrinksFromBasket { get; }

        private void AddDrinks(object drinks)
        {
            BasketViewModel.OrderItems.Add(item: (BasketItemModel)drinks);
        }
        private void DeleteDrinks(object drinks)
        {
            BasketViewModel.OrderItems.Remove(item: (BasketItemModel)drinks);
        }
    }
}
