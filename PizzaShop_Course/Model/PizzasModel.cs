using PizzaShop_Course.Interfaces;
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

        private PizzaSize size;
        public PizzaSize Size
        {
            get => size;
            set
            {
                size = value;
                OnPropertyChanged();
            }
        }
        public enum PizzaSize
        {
            small, middle, big
        }

        private double price;
        public double Price
        {
            get => price;
            set
            {
                OnPropertyChanged();
            }
        }

        private string photopath;
        public string PhotoPath
        {
            get => photopath;
            set
            {
                photopath = value;
                OnPropertyChanged();
            }
        }

        private int mass;
        public int Mass
        {
            get => mass;
            set
            {
                OnPropertyChanged();
            }
        }
        
    }
}
