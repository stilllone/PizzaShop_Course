using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaShop_Course.Interfaces
{
    public interface IHumanDataAuthorization
    {
        string Login { get; set;}
        string Password { get; set;}
        string Email { get; set; }
    }
}
