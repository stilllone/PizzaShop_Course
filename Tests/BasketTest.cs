using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace Tests
{
    [TestClass]
    public class BasketTest
    {
        BasketItemModel orderItem = new BasketItemModel { ItemName = "Margarita", ItemPrice = 100.5 };
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

        DrinksViewModel drinksViewModel = new DrinksViewModel();
        PizzasViewModel pizzasViewModel = new PizzasViewModel();
        MainViewModel mainViewModel = new MainViewModel();
        BasketViewModel basketViewModel = new BasketViewModel();

        
        [WpfTestMethod]
        public void Test_AddPizzaToOrder_WhenAlreadyHaveOne()
        {
            // Arrange
           
            // Act
            pizzasViewModel.AddPizzasToBasket.Execute(pizzaToAdd);
            pizzasViewModel.AddPizzasToBasket.Execute(pizzaToAdd);

            // Assert
            int count = BasketViewModel.OrderItems.FirstOrDefault().ItemCount;
            Assert.AreEqual(2, count);
            Assert.AreEqual(1, BasketViewModel.OrderItems.Count);
            Assert.AreEqual(10.99*2, mainViewModel.BasketTotalPrice);
        }

        [WpfTestMethod]
        public void TestAddingPizzaToBasket()
        {
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            // Act
            pizzasViewModel.AddPizzasToBasket.Execute(pizzaToAdd);

            // Assert
            Assert.IsTrue(BasketViewModel.OrderItems.Any(x => x.ItemName == pizzaToAdd.Name));

        }
        //drink
        [WpfTestMethod]
        public void Test_AddDrinkToOrder_WhenAlreadyHaveOne()
        {
            
            // Act
            pizzasViewModel.AddPizzasToBasket.Execute(pizzaToAdd);
            drinksViewModel.AddDrinksToBasket.Execute(drinkToAdd);

            // Assert
            
            Assert.AreEqual(10.99 + 50.5, mainViewModel.BasketTotalPrice);
            Assert.AreEqual(2, BasketViewModel.OrderItems.Count);
        }

        [WpfTestMethod]
        public void TestAddingDrinksToBasket()
        {
            // Act
            drinksViewModel.AddDrinksToBasket.Execute(drinkToAdd);

            // Assert
            Assert.IsTrue(BasketViewModel.OrderItems.Any(x => x.ItemName == drinkToAdd.Name));
        }

        //items
        [WpfTestMethod]
        public void AddItemToBasket_WithExistingItem_ShouldIncrementItemCount()
        {
            Thread.CurrentThread.SetApartmentState(ApartmentState.STA);
            // Arrange
            var existingItem = new BasketItemModel
            {
                ItemId = 1,
                ItemName = "Margarita",
                ItemPrice = 10,
                ItemSize = "Small",
                ItemCount = 1
            };
            BasketViewModel.OrderItems.Add(existingItem);

            // Act
            pizzasViewModel.AddPizzasToBasket.Execute(new PizzasModel { Id = 1, Name = "Margarita", Price = 10, Size = "Small" });

            // Assert
            Assert.AreEqual(1, BasketViewModel.OrderItems.Count);
            var itemInBasket = BasketViewModel.OrderItems.FirstOrDefault(item => item.ItemName == existingItem.ItemName && item.ItemSize == existingItem.ItemSize);
            Assert.IsNotNull(itemInBasket);
            Assert.AreEqual(existingItem.ItemCount, itemInBasket.ItemCount);
        }
        [WpfTestMethod]
        public void DeleteItemFromBasket_WithExistingItem_ShouldRemoveFromBasket()
        {
            // Arrange
            var itemToDelete = new BasketItemModel
            {
                ItemId = 1,
                ItemName = "Margherita",
                ItemPrice = 10.99,
                ItemSize = "Small",
                ItemCount = 1
            };
            BasketViewModel.OrderItems.Add(itemToDelete);

            // Act
            basketViewModel.Delete.Execute(itemToDelete);

            // Assert
            Assert.AreEqual(0, BasketViewModel.OrderItems.Count);
        }
    }
}