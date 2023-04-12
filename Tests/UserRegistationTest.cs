using MySql.Data.MySqlClient;
using PizzaShop_Course.DataProvider;
using PizzaShop_Course.Model;
using PizzaShop_Course.ViewModel;
using PizzaShop_Course.ViewModel.Administrator;
using PizzaShop_Course.ViewModel.Customer;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Tests
{
    [TestClass]
    public class UserRegistationTest
    {

        [WpfTestMethod]
        public void CreateUserModel_WithValidData()
        {
            // Arrange
            var userRegistration = new UserRegistrationViewModel();
            userRegistration.Login = "testuser1";
            userRegistration.Password = "testpassword";
            userRegistration.FirstName = "Test";
            userRegistration.LastName = "User";
            userRegistration.Email = "testuser@test.com";
            userRegistration.ChangeRoots = false;

            // Act
            userRegistration.CreateUserAndAuthorizeCommand.Execute(userRegistration);

            // Assert
            Assert.IsNotNull(UserViewModel.CurrentUser);
            Assert.AreEqual(UserViewModel.CurrentUser.Login, userRegistration.Login);
            Assert.AreEqual(UserViewModel.CurrentUser.Password, userRegistration.Password);
            Assert.AreEqual(UserViewModel.CurrentUser.FirstName, userRegistration.FirstName);
            Assert.AreEqual(UserViewModel.CurrentUser.LastName, userRegistration.LastName);
            Assert.AreEqual(UserViewModel.CurrentUser.Email, userRegistration.Email);
            Assert.AreEqual(UserViewModel.CurrentUser.ChangeRoots, userRegistration.ChangeRoots);
            var connection = SqlDBConnection.GetDBConnection();
            connection.Open();
            string query = "SELECT COUNT(*) FROM users WHERE login = @login AND pass = @password;";
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@login", userRegistration.Login);
            command.Parameters.AddWithValue("@password", userRegistration.Password);
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            Assert.AreEqual(1, count);

        }

        [WpfTestMethod]
        public void CreateUserAndAuthorize_WithoutSomeParameters()
        {
            // Arrange
            var userRegistration = new UserRegistrationViewModel();
            userRegistration.Login = "testuser";
            userRegistration.FirstName = "Test";
            userRegistration.LastName = "User";
            userRegistration.Email = "testuser@test.com";
            userRegistration.ChangeRoots = false;

            // Act
            userRegistration.CreateUserAndAuthorizeCommand.Execute(userRegistration);

            // Assert
            Assert.IsFalse(UserViewModel.IsAuthorized);
        }
        [WpfTestMethod]
        public void CreateUserAndAuthorize_WithUncorrectEmail()
        {
            // Arrange
            var userRegistration = new UserRegistrationViewModel();
            userRegistration.Login = "testuser";
            userRegistration.Password = "testpassword";
            userRegistration.FirstName = "Test";
            userRegistration.LastName = "User";
            userRegistration.ChangeRoots = false;
            userRegistration.Email = "diisdis";

            // Act
            userRegistration.CreateUserAndAuthorizeCommand.Execute(userRegistration);

            // Assert
            Assert.IsFalse(UserViewModel.IsAuthorized);
        }
        [WpfTestMethod]
        public void CreateUserModel_WithExistLogin()
        {
            // Arrange
            var userRegistration = new UserRegistrationViewModel();
            userRegistration.ChangeRoots = false;
            userRegistration.Login = "testuser1";
            userRegistration.Password = "testpassword";
            userRegistration.FirstName = "Test";
            userRegistration.LastName = "User";
            userRegistration.Email = "testuser@test.com";
            

            // Act
            userRegistration.CreateUserAndAuthorizeCommand.Execute(userRegistration);

            // Assert
            Assert.IsNull(UserViewModel.CurrentUser.Login);
            
        }
    }
}
