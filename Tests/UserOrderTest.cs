using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel;
using PizzaShop_Course.ViewModel.Administrator;
using PizzaShop_Course.ViewModel.Customer;
using PizzaShop_Course.Interfaces.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UserOrderTest
    {
        UserViewModel userVM = new();
        BasketViewModel basketVM = new();
        PizzasViewModel pizzaVM = new();
        DrinksViewModel drinkVM = new();
        OrderViewModel orderVM = new();
        PizzasModel pizzaToAdd = new PizzasModel()
        {
            Id = 1,
            Name = "Pepperoni",
            Ingredients = "Pepperoni, Tomato Sauce, Cheese",
            Size = "Small",
            Price = 10.99,
            Photo = new byte[] { 1, 2, 3 },
            Mass = 300
        };
        DrinksModel drinkToAdd = new DrinksModel()
        {
            Id = 1,
            Name = "Coca-Cola",
            Size = "Small",
            Photo = new byte[] { 1, 2, 3 },
            Price = 50.5
        };

        [WpfTestMethod]
        public void NormalOrderTest()
        {
            //Arrange
            userVM.Login = "usuallyuser";
            userVM.Password = "User12345";

            //arrange + act
            userVM.AuthorizeCommand.Execute(null);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            pizzaVM.AddPizzasToBasket.Execute(pizzaToAdd);
            orderVM.Street = "Usuall street";
            orderVM.City = Towns.Kharkiv.ToString();
            orderVM.Entrance = 5;
            orderVM.Flat = 154;
            orderVM.Floor = 10;
            orderVM.House = "5b";
            orderVM.AddOrderCommand.Execute(null);
            //assert
            Assert.IsTrue(BasketViewModel.OrderItems.Count == 0);
        }
        [WpfTestMethod]
        public void NormalOrderWithOneItemCountTest()
        {
            //Arrange
            userVM.Login = "usuallyuser";
            userVM.Password = "User12345";

            //arrange + act
            userVM.AuthorizeCommand.Execute(null);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            orderVM.Street = "Usuall street";
            orderVM.City = Towns.Kharkiv.ToString();
            orderVM.Entrance = 5;
            orderVM.Flat = 154;
            orderVM.Floor = 10;
            orderVM.House = "5b";
            orderVM.AddOrderCommand.Execute(null);
            //assert
            Assert.IsTrue(BasketViewModel.OrderItems.Count == 0);
        }
        [WpfTestMethod]
        public void BadOrderTest()
        {
            //Arrange
            userVM.Login = "usuallyuser";
            userVM.Password = "User12345";

            //arrange + act
            userVM.AuthorizeCommand.Execute(null);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            orderVM.Street = "Usuall street";
            orderVM.City = Towns.Kharkiv.ToString();
            orderVM.Entrance = 5;
            orderVM.Floor = 10;
            orderVM.House = "5b";
            orderVM.AddOrderCommand.Execute(null);
            //assert
            Assert.IsTrue(BasketViewModel.OrderItems.Count == 1);
        }
        [WpfTestMethod]
        public void BadWithUnAuthorizeOrderTest()
        {
            //arrange + act
            userVM.AuthorizeCommand.Execute(null);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            drinkVM.AddDrinksToBasket.Execute(drinkToAdd);
            orderVM.Street = "Usuall street";
            orderVM.City = Towns.Kharkiv.ToString();
            orderVM.Entrance = 5;
            orderVM.Floor = 10;
            orderVM.House = "5b";
            orderVM.AddOrderCommand.Execute(null);
            //assert
            Assert.IsTrue(BasketViewModel.OrderItems.Count == 1);
        }

    }
}
