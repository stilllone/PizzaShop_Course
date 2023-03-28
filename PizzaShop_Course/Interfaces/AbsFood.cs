using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.Interfaces
{
    public abstract class AbsFood
    {
        int Id { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}
