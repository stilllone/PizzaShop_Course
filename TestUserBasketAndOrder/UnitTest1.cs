using Moq;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel;
using System.Collections.ObjectModel;

namespace TestUserBasketAndOrder
{
    [TestClass]
    public class UnitTest1
    {
        BasketItemModel orderItem = new BasketItemModel {  ItemName = "Margarita", ItemPrice = 10.99 };
        PizzasModel pizzaToAdd = new PizzasModel { Name = "Pepperoni", Price = 12.99 };
        PizzasViewModel pizzasViewModel = new PizzasViewModel();
        MainViewModel mainViewModel = new MainViewModel();
        [TestMethod]
        public void TestMethod1()
        {
        }
        [TestMethod]
        public void Test_AddPizzaToOrder_WhenAlreadyHaveOne()
        {
            // Arrange
            BasketViewModel.OrderItems = new ObservableCollection<BasketItemModel> { orderItem };
            // Act
            pizzasViewModel.AddPizzasToBasket.Execute(AddPizza);

            // Assert
            Assert.AreEqual(2, orderItem.ItemCount);
            Assert.AreEqual(2, BasketViewModel.OrderItems.Count);
            Assert.AreEqual(10.99 + 12.99, mainViewModel.BasketTotalPrice);
        }
        [TestMethod]
        public void TestAddingPizzaToBasket()
        {
            // Arrange
            var pizzasViewModel = new PizzasViewModel();
            // Act
            pizzasViewModel.AddPizza(pizzaToAdd); 

            // Assert
            Assert.IsTrue(BasketViewModel.OrderItems.Any(x => x.ItemName == pizzaToAdd.Name)); 
        }
    }
}