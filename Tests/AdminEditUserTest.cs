using PizzaShop_Course.ViewModel;
using PizzaShop_Course.ViewModel.Administrator;
using PizzaShop_Course.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PizzaShop_Course.DataProvider;
using MySql.Data.MySqlClient;

namespace Tests
{
    [TestClass]
    public class AdminEditUserTest
    {
        UserViewModel userVM = new();
        

        [WpfTestMethod]
        public void AdminEditUser()
        {
            //arrange
            userVM.Login = "admin";
            userVM.Password = "CorrectPass1";
            //act
            userVM.AuthorizeCommand.Execute(null);
            userVM.Email = "asd@gmail.com";
            userVM.FirstName = "idk";
            userVM.UpdateCommand.Execute(null);

            var connection = SqlDBConnection.GetDBConnection();
            string query = "Select COUNT(*) FROM users where email = @newEmail and first_name = @newFirstName and login = @login";
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@newEmail", "asd@gmail.com");
            command.Parameters.AddWithValue("@newFirstName", "idk");
            command.Parameters.AddWithValue("@login", "admin");
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            //assert
            
            Assert.AreEqual(1, count);
        }

        [WpfTestMethod]
        public void AdminEditBadPasswordUser()
        {
            //arrange
            userVM.Login = "admin";
            userVM.Password = "admin12345";
            string newPass = "выфвыфв";
            //act
            userVM.AuthorizeCommand.Execute(null);
            userVM.Password = newPass;
            userVM.UpdateCommand.Execute(null);

            var connection = SqlDBConnection.GetDBConnection();
            string query = "Select COUNT(*) FROM users where pass = @password and login = @login";
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@password", newPass);
            command.Parameters.AddWithValue("@login", "admin");
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();

            //assert
            Assert.AreEqual(0, count);
        }
        [WpfTestMethod]
        public void AdminEditCorrectPasswordUser()
        {
            //arrange
            userVM.Login = "admin";
            userVM.Password = "Admin12345";
            string newPass = "correctPass1";
            //act
            userVM.AuthorizeCommand.Execute(null);
            userVM.Password = newPass;
            userVM.UpdateCommand.Execute(null);

            var connection = SqlDBConnection.GetDBConnection();
            string query = "Select COUNT(*) FROM users where pass = @password and login = @login";
            connection.Open();
            MySqlCommand command = new MySqlCommand(query, connection);
            command.Parameters.AddWithValue("@password", newPass);
            command.Parameters.AddWithValue("@login", userVM.Login);
            int count = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            //assert

            Assert.AreEqual(1, count);
        }
    }
}
