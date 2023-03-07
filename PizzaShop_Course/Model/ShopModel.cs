using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.Model
{
    public class ShopModel : PropertyBase
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
        private string street;
        public string Street
        {
            get => street;
            set
            {
                street = value;
                OnPropertyChanged();
            }
        }
        public double stars;
        public double Starts
        {
            get=> stars;
            set
            {
                stars = value;//make max 5
                OnPropertyChanged();
            }
        }
    }
}
