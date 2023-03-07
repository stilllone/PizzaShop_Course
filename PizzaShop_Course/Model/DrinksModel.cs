﻿using PizzaShop_Course.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        private string photo;
        public string Photo
        {
            get => photo;
            set
            {
                photo = value;
                OnPropertyChanged();
            }
        }
        public enum DrinkSize
        {
            little, small, middle
        }

        private DrinkSize size;
        public DrinkSize Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged();
            }
        }
    }
}