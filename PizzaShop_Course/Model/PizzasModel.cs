﻿using PizzaShop_Course.Interfaces;
using PizzaShop_Course.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PizzaShop_Course.Model
{
    public class PizzasModel : PropertyBase, IFood
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
            get => size;
            set
            {
                size = value;
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
        public FoodSize[] PizzaSize
        {
            get { return (FoodSize[])Enum.GetValues(typeof(FoodSize)); }
        }

    }
}
