using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class AdminDeleteItemsTest
    {
        DeleteFoodViewModel deleteFoodVM = new();
        DrinksDBConnection drinksDB = new();
        PizzasDBConnection pizzasDB = new();
        [WpfTestMethod]
        public void DeleteDrinksTest()
        {
            //arrange
            deleteFoodVM.CurrentItem = FoodItems.Drink;
            if (drinksDB.GetDrinks().Count != 0)
            {
                DrinksModel itemToDelete = drinksDB.GetDrinks()[0];

                //act
                deleteFoodVM.Delete.Execute(itemToDelete);
                //assert
                Assert.IsFalse(drinksDB.GetDrinks().Any(drink => drink.Id == itemToDelete.Id));
            }
            else
                Assert.Fail();
        }
        [WpfTestMethod]
        public void DeleteDrinksWithIncorrectId()
        {
            //arrange
            deleteFoodVM.CurrentItem = FoodItems.Drink;
            DrinksModel drink = new()
            {
                Id = 105,
                Name = "Pepsi",
                Price = 300,
                Size = FoodSize.small.ToString(),
                Photo = null
            };
            //act + assert
            Assert.ThrowsException<Exception>(() => drinksDB.DeleteDrinks(drink.Id));
        }
        [WpfTestMethod]
        public void DeletePizzaTest()
        {
            //arrange
            deleteFoodVM.CurrentItem = FoodItems.Pizza;
            PizzasModel itemToDelete = pizzasDB.GetPizzas()[2];
            //act
            deleteFoodVM.Delete.Execute(itemToDelete);
            //assert
            Assert.IsFalse(drinksDB.GetDrinks().Any(drink => drink.Id == itemToDelete.Id));
        }
        [WpfTestMethod]
        public void DeletePizzaWithIncorrectId()
        {
            //arrange
            deleteFoodVM.CurrentItem = FoodItems.Pizza;
            PizzasModel pizza = new()
            {
                Id = 25,
                Name = "Pepperoni",
                Price = 300,
                Size = FoodSize.small.ToString(),
                Ingredients = "dsadsa",
                Photo = null
            };
            //act + assert
            Assert.ThrowsException<Exception>(() => drinksDB.DeleteDrinks(pizza.Id));
        }
    }
}
