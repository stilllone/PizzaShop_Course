using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel
{
    public class DrinksViewModel : PropertyBase
    {
        private readonly DrinksDBConnection  drinksDBConnection;
        public DrinksViewModel()
        {
            drinksDBConnection = new DrinksDBConnection();
            Task.Run(() => GetDrinksCollectionAsync());
        }
        private async Task GetDrinksCollectionAsync()
        {
            Drinks = await drinksDBConnection.GetDrinksAsync();
        }
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
            var selectedItem = drinks as DrinksModel;
            if (selectedItem != null)
            {
                var newItem = new BasketItemModel()
                {
                    ItemId = selectedItem.Id,
                    ItemName = selectedItem.Name,
                    ItemPrice = selectedItem.Price,
                    ItemSize = selectedItem.Size,
                    ItemPhoto = selectedItem.Photo,
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
        }
    }
}
