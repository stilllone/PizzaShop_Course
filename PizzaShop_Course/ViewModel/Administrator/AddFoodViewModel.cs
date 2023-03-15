﻿using Microsoft.Win32;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PizzaShop_Course.ViewModel.Administrator
{
    public class AddFoodViewModel : PropertyBase
    {
        private PizzasDBConnection pizzasDB;
        private DrinksDBConnection drinksDB;
        public IEnumerable<FoodItems> CBItems
        {
            get
            {
                return Enum.GetValues(typeof(FoodItems)).Cast<FoodItems>();
            }
        }
        public IEnumerable<FoodSize> CBSize
        {
            get
            {
                return Enum.GetValues(typeof(FoodSize)).Cast<FoodSize>();
            }
        }

        private FoodItems currentItem;
        public FoodItems CurrentItem
        {
            get => currentItem;
            set
            {
                if(currentItem != value)
                      currentItem = value;
                OnPropertyChanged(nameof(CurrentItem));
                Debug.WriteLine(CurrentItem);
            }
        }

        public ICommand SelectPhotoCommand { get; set; }
        private void SelectPhoto(object parameter)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Photo = File.ReadAllBytes(openFileDialog.FileName);
            }
        }

        public AddFoodViewModel()
        {
            this.pizzasDB = new PizzasDBConnection();
            this.drinksDB = new DrinksDBConnection();
            SelectPhotoCommand = new RelayCommand(SelectPhoto);
            AddFood = new RelayCommand(AddFoodToDB);
        }

        public ICommand AddFood { get; }
        private void AddFoodToDB(object parameter)
        {
            if (CurrentItem == FoodItems.Pizza)
            {
                PizzasModel pizza = new PizzasModel() { Name = name, Ingredients = ingridients, Size = size, Price = price, Mass = mass, Photo = photo };
                pizzasDB.CreatePizza(pizza);
            }
            else if (CurrentItem == FoodItems.Drink)
            {
                DrinksModel drink = new DrinksModel() { Name = name, Photo = photo, Price = price, Size = size };
                drinksDB.CreateDrinks(drink);
            }
        }
        //private void AddPizzaToDB(object parameter)
        //{
        //    PizzasModel pizza = new PizzasModel() { Name = name, Ingredients = ingridients, Size = size, Price = price, Mass = mass, Photo = photo };
        //    pizzasDB.CreatePizza(pizza);
        //}
        #region prop
        private string itemVisibility;
        public string ItemVisibility
        {
            get => itemVisibility;
            set
            {
                if (CurrentItem == FoodItems.Pizza)
                    itemVisibility = "Visible";
                else if(CurrentItem == FoodItems.Drink)
                    itemVisibility = "Hidden";
                OnPropertyChanged(nameof(ItemVisibility));
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
            get => size;
            set
            {
                size = value;
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
        #endregion 
    }
}
