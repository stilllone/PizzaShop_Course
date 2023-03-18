using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.Model
{
    public class BasketItemModel
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public byte[] ItemPhoto { get; set; }
        public string ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemSize { get; set; }
    }
}
