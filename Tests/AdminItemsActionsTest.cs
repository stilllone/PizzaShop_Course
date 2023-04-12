using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Interfaces.Enums;
using PizzaShop_Course.ViewModel.Administrator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tests
{
    [TestClass]
    public class AdminItemsActionsTest
    {
        AddFoodViewModel addFoodVM = new();
        PizzasDBConnection pizzaDB = new();
        DrinksDBConnection drinksDB = new();
        [WpfTestMethod]
        public void NormalAddItemTest()
        {
            //arrange
            addFoodVM.CurrentItem = FoodItems.Pizza;
            addFoodVM.Name = "Italian";
            addFoodVM.Ingredients = "asd";
            addFoodVM.Price = 100;
            addFoodVM.Mass = 500;
            addFoodVM.Photo = null;

            //act
            addFoodVM.AddFood.Execute(null);

            //assert
            Assert.IsTrue(pizzaDB.GetPizzas().Any(p => p.Name == "Italian" && p.Ingredients == "asd" && p.Price == 100));
        }
        [WpfTestMethod]
        public void WithNullNameAddPizza()
        {
            //arrange
            addFoodVM.CurrentItem = FoodItems.Pizza;
            addFoodVM.Name = null;
            addFoodVM.Ingredients = "asd";
            addFoodVM.Price = 100;
            addFoodVM.Mass = 200;
            addFoodVM.Photo = null;

            //act
            try
            {
                addFoodVM.AddFood.Execute(null);
            }
            catch (Exception ex)
            {
                //assert
                Assert.IsFalse(pizzaDB.GetPizzas().Any(p => p.Name == "Italian" && p.Ingredients == "asd" && p.Price == 100 && p.Mass == 200));
            }
        }
        [WpfTestMethod]
        public void WithNullIngridientsAddPizza()
        {
            //arrange
            PizzasDBConnection pizzaDB = new();
            addFoodVM.CurrentItem = FoodItems.Pizza;
            addFoodVM.Name = "American";
            addFoodVM.Ingredients = null;
            addFoodVM.Price = 100;
            addFoodVM.Mass = 200;
            addFoodVM.Photo = null;

            //act
            try
            {
                addFoodVM.AddFood.Execute(null);
            }
            catch (Exception ex)
            {
                //assert
                Assert.IsFalse(pizzaDB.GetPizzas().Any(p => p.Name == "American" && p.Price == 100 && p.Mass == 200));
            }
        }
        [WpfTestMethod]
        public void NormalAddDrinks()
        {
            //arrange
            addFoodVM.CurrentItem = FoodItems.Drink;
            addFoodVM.Name = "Coca-Cola";
            addFoodVM.Price = 300;
            addFoodVM.Size = FoodSize.small.ToString();
            addFoodVM.Photo = null;

            //act
            try
            {
                addFoodVM.AddFood.Execute(null);
            }
            catch (Exception ex)
            {
                //assert
                Assert.IsTrue(drinksDB.GetDrinks().Any(p => p.Name == "American" && p.Price == 300 && p.Size == FoodSize.small.ToString()));
            }
        }
        [WpfTestMethod]
        public void WithNullNameAddDrinks()
        {
            //arrange
            addFoodVM.CurrentItem = FoodItems.Drink;
            addFoodVM.Name = null;
            addFoodVM.Price = 300;
            addFoodVM.Size = FoodSize.small.ToString();
            addFoodVM.Photo = null;

            //act
            try
            {
                addFoodVM.AddFood.Execute(null);
            }
            catch (Exception ex)
            {
                //assert
                Assert.IsFalse(drinksDB.GetDrinks().Any(p => p.Name == null && p.Price == 300 && p.Size == FoodSize.small.ToString()));
            }
        }
        
    }
}
