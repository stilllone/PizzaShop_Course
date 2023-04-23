using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.Model
{
    public class BasketItemModel : PropertyBase
    {
        
        public int ItemId { get; set; }
        public byte[] ItemPhoto { get; set; }
        public string ItemName { get; set; }

        private double itemPrice;
        public double ItemPrice
        {
            get { return itemPrice; }
            set
            {
                if (itemPrice != value)
                {
                    itemPrice = value;
                    OnPropertyChanged(nameof(ItemPrice));
                }
            }
        }
        public string ItemSize { get; set; }
        private int itemCount;
        public int ItemCount 
        {
            get => itemCount;
            set 
            {
                itemCount = value;
                OnPropertyChanged(nameof(ItemCount));
            }
        }
    }
}
